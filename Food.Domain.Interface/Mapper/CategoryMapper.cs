using Food.Domain.Business.DTO;
using Food.Domain.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.Mapper
{
    public static class CategoryMapper
    {
        public static CategoryDTO MapDTO(CategoryEntity dto)
        {
            if (dto != null)
                return AttributesDTO(dto);
            return null;
        }
        public static List<CategoryDTO> MapListDTO(List<CategoryEntity> entity)
        {
            List<CategoryDTO> categories = new();
            if (entity.Count > 0)
                foreach (CategoryEntity item in entity)
                    categories.Add(AttributesDTO(item));
            return categories;
        }

        public static CategoryEntity MapEntity(CategoryDTO dto)
        {
            if (dto != null)
                return AttributesEntity(dto);
            return null;
        }
        public static List<CategoryEntity> MapListEntity(List<CategoryDTO> entity)
        {
            List<CategoryEntity> entities = new();
            if (entity.Count > 0)
            {
                foreach (CategoryDTO item in entity)
                    entities.Add(AttributesEntity(item));
            }
            return entities;
        }

        private static CategoryDTO AttributesDTO(CategoryEntity entity)
        {
            var dto = new CategoryDTO()
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Descripcion = entity.Descripcion,
                Dishes = entity.Dishes != null ? DishMapper.MapListCategoryDTO(entity.Dishes) : new List<DishCategoryDTO>()
            };
            return dto;
        }
        private static CategoryEntity AttributesEntity(CategoryDTO dto)
        {
            var Dish = new CategoryEntity()
            {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };
            return Dish;
        }
    }
}
