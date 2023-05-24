using Food.Domain.Business.DTO;
using Food.Domain.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.Mapper
{
    public class RestaurantEmployeeMapper
    {
        public static RestaurantEmployeeDTO MapDTO(RestaurantEmployeeEntity dto)
        {
            var model = new RestaurantEmployeeDTO();
            if (dto != null)
                return model = AttributesDTO(dto);
            return null;
        }
        public static List<RestaurantEmployeeDTO> MapListDTO(List<RestaurantEmployeeEntity> entity)
        {
            List<RestaurantEmployeeDTO> restaurants = new();
            if (entity.Count > 0)
                foreach (RestaurantEmployeeEntity item in entity)
                    restaurants.Add(AttributesDTO(item));
            return restaurants;
        }

        public static RestaurantEmployeeEntity MapEntity(RestaurantEmployeeDTO dto)
        {
            var model = new RestaurantEmployeeEntity();
            if (dto != null)
                return model = AttributesEntity(dto);
            return null;
        }
        public static List<RestaurantEmployeeEntity> MapListEntity(List<RestaurantEmployeeDTO> entity)
        {
            List<RestaurantEmployeeEntity> entities = new();
            if (entity.Count > 0)
            {
                foreach (RestaurantEmployeeDTO item in entity)
                    entities.Add(AttributesEntity(item));
            }
            return entities;
        }

        private static RestaurantEmployeeDTO AttributesDTO(RestaurantEmployeeEntity entity)
        {
            var dto = new RestaurantEmployeeDTO()
            {
                Id = entity.Id,
                IdPersona = entity.IdPersona,
                IdRestaurante = entity.IdRestaurante,
            };
            return dto;
        }
        private static RestaurantEmployeeEntity AttributesEntity(RestaurantEmployeeDTO dto)
        {
            var restaurant = new RestaurantEmployeeEntity()
            {
                Id = dto.Id,
                IdPersona = dto.IdPersona,
                IdRestaurante = dto.IdRestaurante,
            };
            return restaurant;
        }
    }
}
