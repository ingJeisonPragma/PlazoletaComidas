using Food.Domain.Business.DTO;
using Food.Domain.Interface.Entities;
using Food.Domain.Interface.Exceptions;
using Food.Domain.Interface.IRepository;
using Food.Domain.Interface.IServices;
using Food.Domain.Interface.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Services.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDishRepository _dishRepository;
        private readonly IOrderDishRepository _orderDishRepository;
        private readonly IRestaurantEmployeeServices _restaurantEmployeeServices;

        public OrderServices(IOrderRepository orderRepository,
            IDishRepository dishRepository, IOrderDishRepository orderDishRepository,
            IRestaurantEmployeeServices restaurantEmployeeServices)
        {
            this._orderRepository = orderRepository;
            this._dishRepository = dishRepository;
            this._orderDishRepository = orderDishRepository;
            this._restaurantEmployeeServices = restaurantEmployeeServices;
        }

        public async Task<StandardResponse> GetPending(int IdEmployee, int page, int take)
        {
            //Validar Los restaurantes asociados al Empleado
            var standard = await _restaurantEmployeeServices.GetRestaurantEmployee(IdEmployee);

            var restautant = standard.Result.MapTo<RestaurantEmployeeDTO>();
            if (restautant == null)
                throw new DomainValidateException(new StandardResponse { IsSuccess = false, Message = "Eror al mapear el restaurante del empleado." });

            //Buscar los estados pendientes
            var orderEntities = await _orderRepository.GetOrderState(restautant.IdRestaurante, "PENDIENTE", page, take);

            var orderMap = orderEntities.MapTo<PaginatedListDTO<OrderDTO>>();

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
    }
}
