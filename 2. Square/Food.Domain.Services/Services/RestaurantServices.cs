using Food.Domain.Business.DTO;
using Food.Domain.Business.UserProxyDTO;
using Food.Domain.Interface.Entities;
using Food.Domain.Interface.Exceptions;
using Food.Domain.Interface.IRepository;
using Food.Domain.Interface.IServices;
using Food.Domain.Interface.IServices.IUserProxy;
using Food.Domain.Interface.Mapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Food.Domain.Services.Services
{
    public class RestaurantServices : IRestaurantServices
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUserServices _userServices;

        public RestaurantServices(IRestaurantRepository restaurantRepository,
            IUserServices userServices)
        {
            this._restaurantRepository = restaurantRepository;
            this._userServices = userServices;
        }

        public async Task<StandardResponse> CreateRestaurant(RestaurantDTO restaurant)
        {
            var response = await _userServices.ValidateUserOwner(Convert.ToInt32(restaurant.IdPropietario));
            if (!response.IsSuccess)
                throw new DomainValidateException(response);

            var restaurantEntity = RestaurantMapper.MapEntity(restaurant);
            var result = await _restaurantRepository.Add(restaurantEntity);

            if (result != null)
                return new StandardResponse { IsSuccess = true, Message = "Se creo correctamente el Restaurante." };
            else
                throw new DomainValidateException(new StandardResponse { IsSuccess = false, Message = "Error creando el Restaurante." });
        }

        public async Task<StandardResponse> GetByIdRestaurant(int Id)
        {
            var resultRestaurant = RestaurantMapper.MapDTO(await _restaurantRepository.GetById(Id));

            if (resultRestaurant != null)
                return new StandardResponse { IsSuccess = true, Message = "Se encontró el restaurante.", Result = JsonConvert.SerializeObject(resultRestaurant) };
            else
                throw new DomainValidateException(new StandardResponse { IsSuccess = false, Message = "El resturante no se encuentra creado." });
        }

        public async Task<StandardResponse> GetValidateRestaurantOwner(int IdRestaurante, int IdPropietario)
        {
            var Restaurant = await GetByIdRestaurant(IdRestaurante);

            if (!Restaurant.IsSuccess)
                throw new DomainValidateException(Restaurant);

            var result = JsonConvert.DeserializeObject<RestaurantDTO>(Restaurant.Result.ToString());
            if (result.IdPropietario != IdPropietario)
                throw new DomainValidateException(new StandardResponse { IsSuccess = false, Message = "El Propietario no está asociado al restaurante." });

            return new StandardResponse { IsSuccess = true, Message = "El restaurante pertenece al propietario." };
        }

        public async Task<StandardResponse> GetListRestaurant(int page, int take)
        {
            var resultRestaurant = (await _restaurantRepository.GetAll(page, take)).MapTo<PaginatedListDTO<RestaurantListDTO>>();

            //var resultRestaurant = result.MapTo<PaginatedListDTO<RestaurantListDTO>>();
            // DtoMapperExtension.MapTo<PaginatedListDTO<RestaurantListDTO>>(result);

            if (resultRestaurant.Items.Count > 0)
                return new StandardResponse { IsSuccess = true, Message = "Se encontraron los restaurantes.", Result = resultRestaurant };
            else
                throw new DomainValidateException(new StandardResponse { IsSuccess = false, Message = "No hay restaurantes creados." });
        }
    }
}
