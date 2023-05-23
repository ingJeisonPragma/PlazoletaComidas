using Food.Domain.Business.DTO;
using Food.Domain.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.Mapper
{
    public static class DishMapper
    {
        public static DishDTO MapDTO(DishEntity dto)
        {
            var model = new DishDTO();
            if (dto != null)
                model = AttributesDTO(dto);
            return model;
        }
        public static List<DishDTO> MapListDTO(List<DishEntity> entity)
        {
            List<DishDTO> Dishs = new();
            if (entity.Count > 0)
                foreach (DishEntity item in entity)
                    Dishs.Add(AttributesDTO(item));
            return Dishs;
        }

        public static DishEntity MapEntity(DishDTO dto)
        {
            var model = new DishEntity();
            if (dto != null)
                model = AttributesEntity(dto);
            return model;
        }
        public static List<DishEntity> MapListEntity(List<DishDTO> entity)
        {
            List<DishEntity> entities = new();
            if (entity.Count > 0)
            {
                foreach (DishDTO item in entity)
                    entities.Add(AttributesEntity(item));
            }
            return entities;
        }

        private static DishDTO AttributesDTO(DishEntity entity)
        {
            var dto = new DishDTO()
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Descripcion = entity.Descripcion,
                Precio = Convert.ToInt32(entity.Precio),
                IdCategoria = entity.IdCategoria,
                IdRestaurant = entity.IdRestaurant,
                urlImagen = entity.urlImagen,
                Activo = entity.Activo
            };
            return dto;
        }
        private static DishEntity AttributesEntity(DishDTO dto)
        {
            var Dish = new DishEntity()
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = dto.Precio,
                IdCategoria = dto.IdCategoria,
                IdRestaurant = dto.IdRestaurant,
                urlImagen = dto.urlImagen,
                Activo = dto.Activo
            };
            return Dish;
        }
    }
}
