using Food.Domain.Business.DTO;
using Food.Domain.Interface.Exceptions;
using Food.Domain.Interface.IRepository;
using Food.Domain.Interface.IServices;
using Food.Domain.Interface.IServices.IUserProxy;
using Food.Domain.Interface.Mapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Services.Services
{
    public class DishServices : IDishServices
    {
        private readonly IDishRepository _dishRepository;
        private readonly IUserServices _userServices;
        private readonly IRestaurantServices _restaurantServices;

        public DishServices(IDishRepository dishRepository,
            IUserServices userServices,
            IRestaurantServices restaurantServices)
        {
            this._dishRepository = dishRepository;
            this._userServices = userServices;
            this._restaurantServices = restaurantServices;
        }
        public async Task<StandardResponse> CreateDish(DishDTO dish)
        {
            //Validar propietario exista
            var response = await _userServices.ValidateUserOwner(dish.IdPropietario);
            if (!response.IsSuccess)
                throw new DomainValidateException(response);
            //var ResponseRestaurant = await _restaurantServices.GetByIdRestaurant(dish.IdRestaurant);

            //var restaurant = (RestaurantDTO)ResponseRestaurant.Result;

            ////Validar propietario asociado a restaurante
            ////Comparar el propietario del restaurante corresponda al enviado.
            //if (restaurant.IdPropietario != dish.IdPropietario)
            //{
            //    response.IsSuccess = false;
            //    response.Message = "El Propietario no está asignado a este restaurante.";
            //    throw new DomainValidateException(response);
            //}

            //Validar propietario asociado a restaurante
            var ResponseRestaurant = await _restaurantServices.GetValidateRestaurantOwner(dish.IdRestaurant, dish.IdPropietario);

            dish.Activo = true;
            var DishEntityData = DishMapper.MapEntity(dish);
            await _dishRepository.Add(DishEntityData);

            response.IsSuccess = true;
            response.Message = "El Plato fue creado correctamente.";
            return response;

        }

        public async Task<StandardResponse> UpdateDish(DishUpdateDTO dish)
        {
            StandardResponse response = new();
            //Buscar el plato
            var entity = await _dishRepository.GetById(dish.IdPlato);

            if (entity == null)
            {
                response.IsSuccess = false;
                response.Message = "El plato no existe.";
                throw new DomainValidateException(response);
            }

            entity.Precio = dish.Precio;
            entity.Descripcion = dish.Descripcion;
            //Actualizar Plato
            await _dishRepository.Update(entity);

            response.IsSuccess = true;
            response.Message = "El Plato fue actualizado correctamente.";
            return response;

        }

        public async Task<StandardResponse> UpdateDishState(DishUpdateStateDTO dish, int IdPropietario)
        {
            StandardResponse response = new();
            //Buscar el plato
            var entity = await _dishRepository.GetById(dish.IdPlato);

            if (entity == null)
            {
                response.IsSuccess = false;
                response.Message = "El plato no existe.";
                throw new DomainValidateException(response);
            }

            //Validar propietario asociado a restaurante
            await _restaurantServices.GetValidateRestaurantOwner(entity.IdRestaurant, IdPropietario);

            entity.Activo = dish.Activo;
            //Actualizar Plato
            await _dishRepository.Update(entity);

            response.IsSuccess = true;
            response.Message = "El Plato fue actualizado correctamente.";
            return response;

        }
    }
}
