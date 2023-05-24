using Food.Domain.Business.DTO;
using Food.Domain.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.Mapper
{
    public class OrderMapper
    {
        public static OrderDTO MapDTO(OrderEntity dto)
        {
            if (dto != null)
                return AttributesDTO(dto);
            return null;
        }
        public static List<OrderDTO> MapListDTO(List<OrderEntity> entity)
        {
            List<OrderDTO> categories = new();
            if (entity.Count > 0)
                foreach (OrderEntity item in entity)
                    categories.Add(AttributesDTO(item));
            return categories;
        }

        public static OrderEntity MapEntity(OrderDTO dto)
        {
            if (dto != null)
                return AttributesEntity(dto);
            return null;
        }
        public static List<OrderEntity> MapListEntity(List<OrderDTO> entity)
        {
            List<OrderEntity> entities = new();
            if (entity.Count > 0)
            {
                foreach (OrderDTO item in entity)
                    entities.Add(AttributesEntity(item));
            }
            return entities;
        }

        private static OrderDTO AttributesDTO(OrderEntity entity)
        {
            var dto = new OrderDTO()
            {
                IdCliente = entity.IdCliente,
                Fecha = entity.Fecha,
                Estado = entity.Estado,
                IdChef = entity.IdChef,
                IdRestaurante = entity.IdRestaurante,
                //orderDishes = entity.OrderDishes != null ? order
            };
            return dto;
        }
        private static OrderEntity AttributesEntity(OrderDTO dto)
        {
            var Dish = new OrderEntity()
            {
                IdCliente = dto.IdCliente,
                Fecha = dto.Fecha,
                Estado = dto.Estado,
                IdChef = dto.IdChef,
                IdRestaurante = dto.IdRestaurante
            };
            return Dish;
        }
    }
}
