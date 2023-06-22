using Food.Domain.Business.DTO;
using Food.Domain.Business.DTO.OrderDish;
using Food.Domain.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Domain.Interface.Mapper
{
    public class OrderDishMapper
    {
        public static WriteOrderDishDTO? MapDTO(OrderDishEntity dto)
        {
            if (dto != null)
                return AttributesDTO(dto);
            return null;
        }
        public static List<WriteOrderDishDTO> MapListDTO(List<OrderDishEntity> entity)
        {
            List<WriteOrderDishDTO> writeOrders = new();
            if (entity.Count > 0)
                foreach (OrderDishEntity item in entity)
                    writeOrders.Add(AttributesDTO(item));
            return writeOrders;
        }

        public static OrderDishEntity? MapEntity(WriteOrderDishDTO dto)
        {
            if (dto != null)
                return AttributesEntity(dto);
            return null;
        }
        public static List<OrderDishEntity> MapListEntity(List<WriteOrderDishDTO> entity)
        {
            List<OrderDishEntity> entities = new();
            if (entity.Count > 0)
            {
                foreach (WriteOrderDishDTO item in entity)
                    entities.Add(AttributesEntity(item));
            }
            return entities;
        }

        private static WriteOrderDishDTO AttributesDTO(OrderDishEntity entity)
        {
            var dto = new WriteOrderDishDTO()
            {
                IdPedido = entity.IdPedido,
                IdPlato = Convert.ToInt32(entity.IdPlato),
                Cantidad = entity.Cantidad,
                //OrderDishDishes = entity.OrderDishDishes != null ? OrderDish
            };
            return dto;
        }
        private static OrderDishEntity AttributesEntity(WriteOrderDishDTO dto)
        {
            var orderDish = new OrderDishEntity()
            {
                IdPedido = dto.IdPedido,
                IdPlato = dto.IdPlato,
                Cantidad = dto.Cantidad,
            };
            return orderDish;
        }
    }
}
