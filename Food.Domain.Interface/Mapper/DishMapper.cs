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
        public static DishDTO? MapDTO(DishEntity dto)
        {
            if (dto != null)
                return AttributesDTO(dto);
            return null;
        }
        public static List<DishDTO> MapListDTO(List<DishEntity> entity)
        {
            List<DishDTO> Dishs = new();
            if (entity.Count > 0)
                foreach (DishEntity item in entity)
                    Dishs.Add(AttributesDTO(item));
            return Dishs;
        }

        public static DishEntity? MapEntity(DishDTO dto)
        {
            if (dto != null)
                return AttributesEntity(dto);
            return null;
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
                UrlImagen = entity.urlImagen,
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
                urlImagen = dto.UrlImagen,
                Activo = dto.Activo
            };
            return Dish;
        }

        #region Agrupación Categoria y Producto
        public static List<DishCategoryDTO> MapListCategoryDTO(List<DishEntity> entity)
        {
            List<DishCategoryDTO> Dishs = new();
            if (entity.Count > 0)
                foreach (DishEntity item in entity)
                    Dishs.Add(AttributesCategoryDTO(item));
            return Dishs;
        }

        private static DishCategoryDTO AttributesCategoryDTO(DishEntity entity)
        {
            var dto = new DishCategoryDTO()
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Descripcion = entity.Descripcion,
                Precio = Convert.ToInt32(entity.Precio),
                IdRestaurant = entity.IdRestaurant,
                UrlImagen = entity.urlImagen,
            };
            return dto;
        }
        #endregion
    }
}
