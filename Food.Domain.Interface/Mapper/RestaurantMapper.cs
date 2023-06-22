using Food.Domain.Business.DTO;
using Food.Domain.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.Mapper
{
    public class RestaurantMapper
    {
        public static RestaurantDTO MapDTO(RestaurantEntity dto)
        {
            var model = new RestaurantDTO();
            if (dto != null)
                return model = AttributesDTO(dto);
            return null;
        }
        public static List<RestaurantDTO> MapListDTO(List<RestaurantEntity> entity)
        {
            List<RestaurantDTO> restaurants = new();
            if (entity.Count > 0)
                foreach (RestaurantEntity item in entity)
                    restaurants.Add(AttributesDTO(item));
            return restaurants;
        }

        public static RestaurantEntity MapEntity(RestaurantDTO dto)
        {
            var model = new RestaurantEntity();
            if (dto != null)
                model = AttributesEntity(dto);
            return model;
        }
        public static List<RestaurantEntity> MapListEntity(List<RestaurantDTO> entity)
        {
            List<RestaurantEntity> entities = new();
            if (entity.Count > 0)
            {
                foreach (RestaurantDTO item in entity)
                    entities.Add(AttributesEntity(item));
            }
            return entities;
        }

        private static RestaurantDTO AttributesDTO(RestaurantEntity entity)
        {
            var dto = new RestaurantDTO()
            {
                Id = entity.Id,
                Nit = entity.Nit,
                Nombre = entity.Nombre,
                Direccion = entity.Direccion,
                Telefono = entity.Telefono,
                IdPropietario = entity.IdPropietario,
                urlLogo = entity.urlLogo,
            };
            return dto;
        }
        private static RestaurantEntity AttributesEntity(RestaurantDTO dto)
        {
            var restaurant = new RestaurantEntity()
            {
                Id = dto.Id,
                Nit = dto.Nit,
                Nombre = dto.Nombre,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                IdPropietario = dto.IdPropietario,
                urlLogo = dto.urlLogo,
            };
            return restaurant;
        }
    }
}
