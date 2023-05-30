﻿using Food.Domain.Business.DTO;
using Food.Domain.Business.DTO.Order;
using Food.Domain.Business.DTO.OrderDish;
using Food.Domain.Interface.Entities;
using Food.Domain.Interface.Exceptions;
using Food.Domain.Interface.IRepository;
using Food.Domain.Interface.IServices;
using Food.Domain.Interface.IServices.ITwilioProxy;
using Food.Domain.Interface.IServices.IUserProxy;
using Food.Domain.Interface.Mapper;
using Food.Domain.Services.Services.TwilioProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Clients;

namespace Food.Domain.Services.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IOrderDishRepository _orderDishRepository;
        private readonly IRestaurantEmployeeServices _restaurantEmployeeServices;
        private readonly ITwilioServices _twilioServices;
        private readonly IUserServices _userServices;

        public OrderServices(IOrderRepository orderRepository,
            IDishRepository dishRepository, IOrderDishRepository orderDishRepository,
            IRestaurantEmployeeServices restaurantEmployeeServices,
            ITwilioServices twilioServices, IUserServices userServices)
        {
            this._orderRepository = orderRepository;
            this._dishRepository = dishRepository;
            this._orderDishRepository = orderDishRepository;
            this._restaurantEmployeeServices = restaurantEmployeeServices;
            this._twilioServices = twilioServices;
            this._userServices = userServices;
        }

        public async Task<StandardResponse> GetPending(int IdEmployee, int page, int take)
        {
            //Validar el restaurante asociado al Empleado
            var standard = await _restaurantEmployeeServices.GetRestaurantEmployee(IdEmployee);

            var restautant = standard.Result.MapTo<RestaurantEmployeeDTO>();
            if (restautant == null)
                throw new DomainValidateException(new StandardResponse { IsSuccess = false, Message = "Error al mapear el restaurante del empleado." });

            //Buscar los ordenes con estados pendientes
            var orderEntities = await _orderRepository.GetOrderState(restautant.IdRestaurante, "PENDIENTE", page, take);

            var orderMap = orderEntities.MapTo<PaginatedListDTO<QueryOrderDTO>>();
            
            //Cargar los platos a realizar
            foreach (var item in orderMap.Items)
            {
                var orderdish = await _orderDishRepository.GetOrderDish(item.Id);
                item.Dishes = new();
                foreach (var itemDish in orderdish)
                {
                    var Dish = await _dishRepository.GetById(Convert.ToInt32(itemDish.IdPlato));
                    item.Dishes.Add(DishMapper.MapDTO(Dish));
                }
            }
            
            if (orderMap.Total > 0)
                return new StandardResponse { IsSuccess = true, Message = "Lista de pedidos en estado Pendientes.", Result = orderMap };
            else
                return new StandardResponse { IsSuccess = false, Message = "No se tienen pedidos Pendientes en el restaurante." };
        }

        public async Task<StandardResponse> CreateOrder(OrderDTO order)
        {
            //Validar que el cliente no tenga más de un pedido creado en Estado (PENDIENTE, EN_PREPARACION, LISTO)
            var orderEntities = await _orderRepository.ValidateOrderCustomer(order.IdCliente);

            var orderCustomers = orderEntities.Where(o => o.Estado.Contains("PENDIENTE") || o.Estado.Contains("EN_PREPARACION") || o.Estado.Contains("LISTO")).ToList();
            if (orderCustomers.Count > 0)
                throw new DomainValidateException(new StandardResponse { IsSuccess = false, Message = "El cliente aun tiene pedidos sin entregar." });

            //Validar que los platos correspondan al restaurante.
            foreach (var dish in order.orderDishes)
            {
                var dishEntity = await _dishRepository.GetById(dish.IdPlato);

                if (dishEntity == null)
                    throw new DomainValidateException(new StandardResponse { IsSuccess = false, Message = $"El plato {dish.IdPlato} no existe o está Inactivo." });
                if (dishEntity.IdRestaurant != order.IdRestaurante)
                    throw new DomainValidateException(new StandardResponse { IsSuccess = false, Message = $"El plato {dishEntity.Nombre} no pertenece al restaurante {dishEntity.Restaurant.Nombre}." });
            }

            //Asignar estado Pendiente al pedido
            order.Estado = "PENDIENTE";
            order.IdChef = null;
            var orderEntity = order.MapTo<OrderEntity>();//OrderMapper.MapEntity(order);

            //Crear el pedido
            var resultEntity = await _orderRepository.AddOrder(orderEntity);

            //Crear plato por pedido
            foreach (var item in order.orderDishes)
            {
                item.IdPedido = resultEntity.Id;
                var orderDishEntity = item.MapTo<OrderDishEntity>();// OrderDishMapper.MapEntity(order);
                var resul = await _orderDishRepository.AddOrderDish(orderDishEntity);
            }

            return new StandardResponse { IsSuccess = true, Message = "Pedido creado exitosamente.", Result = resultEntity.Id };
        }

        public async Task<StandardResponse> UpdateOrder(List<UpdateOrderDTO> orders, int IdEmployee)
        {
            foreach (var item in orders)
            {
                //Buscar información de la Orden
                var order = await _orderRepository.GetById(item.IdPedido);
                if (order == null)
                    throw new DomainValidateException(new StandardResponse { IsSuccess = false, Message = $"El pedido {item.IdPedido} no existe." });
                if (order.Estado != "PENDIENTE")
                    throw new DomainValidateException(new StandardResponse { IsSuccess = false, Message = $"El pedido {item.IdPedido} ya no se encuentra en estado PENDIENTE." });

                //Validar el restaurante asociado al Empleado
                var standard = await _restaurantEmployeeServices.GetRestaurantEmployee(IdEmployee);

                var restautant = standard.Result.MapTo<RestaurantEmployeeDTO>();
                if (restautant == null)
                    throw new DomainValidateException(new StandardResponse { IsSuccess = false, Message = "Error al mapear el restaurante del empleado." });

                //Validar que el restaurante del pedido sea el mismo del restaurante del empleado
                if (restautant.IdRestaurante != order.IdRestaurante)
                    throw new DomainValidateException(new StandardResponse { IsSuccess = false, Message = $"El pedido {item.IdPedido} no pertenece al Restaurante del Empleado {IdEmployee}." });

                //Asignar estado EN_PREPARACION y empleado al pedido
                order.Estado = "EN_PREPARACION";
                order.IdChef = restautant.Id;

                var resultEntity = await _orderRepository.UpdateOrder(order);

            }

            return new StandardResponse { IsSuccess = true, Message = "Asignaciones de pedido exitosa." };
        }

        public async Task<StandardResponse> UpdateOrderOK(List<UpdateOrderDTO> orders, int IdEmployee)
        {
            foreach (var item in orders)
            {
                //Buscar información de la Orden
                var order = await _orderRepository.GetById(item.IdPedido);
                if (order == null)
                    throw new DomainValidateException(new StandardResponse { IsSuccess = false, Message = $"El pedido {item.IdPedido} no existe." });
                if (order.Estado != "EN_PREPARACION")
                    throw new DomainValidateException(new StandardResponse { IsSuccess = false, Message = $"El pedido {item.IdPedido} no se encuentra en estado EN_PREPARACION." });

                //Validar el restaurante asociado al Empleado
                var standard = await _restaurantEmployeeServices.GetRestaurantEmployee(IdEmployee);

                var restautant = standard.Result.MapTo<RestaurantEmployeeDTO>();
                if (restautant == null)
                    throw new DomainValidateException(new StandardResponse { IsSuccess = false, Message = "Error al mapear el restaurante del empleado." });

                //Validar que el restaurante del pedido sea el mismo del restaurante del empleado
                if (restautant.IdRestaurante != order.IdRestaurante)
                    throw new DomainValidateException(new StandardResponse { IsSuccess = false, Message = $"El pedido {item.IdPedido} no pertenece al Restaurante del Empleado {IdEmployee}." });

                string Pin = new Random().Next(0, 1000000).ToString("D6");
                string msg = $"El código de verificación del pedido {order.Id} es el {Pin}";

                //Obtener el número de celular del cliente
                var user = await _userServices.ValidateUserCustomer(order.IdCliente);
                if (user == null)
                    throw new DomainValidateException(new StandardResponse { IsSuccess = false, Message = "Error buscando los datos del cliente." });

                //Enviar SMS al usuario
                await _twilioServices.SendSMS(user.Celular, msg);


                //Asignar estado LISTO y empleado al pedido
                order.Estado = "LISTO";
                order.IdChef = restautant.Id;

                var resultEntity = await _orderRepository.UpdateOrder(order);

            }

            return new StandardResponse { IsSuccess = true, Message = "Asignaciones de pedido exitosa." };
        }
    }
}
