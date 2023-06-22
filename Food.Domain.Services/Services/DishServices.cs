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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserServices _userServices;
        private readonly IRestaurantServices _restaurantServices;

        public DishServices(IDishRepository dishRepository,
            ICategoryRepository categoryRepository,
            IUserServices userServices,
            IRestaurantServices restaurantServices)
        {
            this._dishRepository = dishRepository;
            this._categoryRepository = categoryRepository;
            this._userServices = userServices;
            this._restaurantServices = restaurantServices;
        }
        public async Task<StandardResponse> CreateDish(DishDTO dish, int IdPropietario)
        {
            //Validar propietario exista
            var response = new StandardResponse(); //await _userServices.ValidateUserOwner(IdPropietario);
            //if (!response.IsSuccess)
            //    throw new DomainValidateException(response);

            //Validar propietario asociado a restaurante
            var ResponseRestaurant = await _restaurantServices.GetValidateRestaurantOwner(dish.IdRestaurant, IdPropietario);

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

        public async Task<StandardResponse> GetDishByCategory(int IdRestaurant)
        {
            //Validar propietario exista
            var response = await _categoryRepository.GetAll(IdRestaurant);

            var categories = CategoryMapper.MapListDTO(response);

            if (response.Count > 0)
                return new StandardResponse { IsSuccess = true, Message = "Lista de Platos por categoria.", Result = categories };
            else
                throw new DomainValidateException(new StandardResponse { IsSuccess = false, Message = "No hay Platos asociados al restaurante." });

        }
    }
}
