using User.Domain.Business.DTO;
using User.Domain.Interface.Entities;

namespace User.Domain.Interface.Mapper
{
    public class RolMapper
    {
        public static List<RolDTO> MapList(List<RolEntity> entity)
        {
            List<RolDTO> lsRols = new();
            if (entity.Count > 0)
            {
                foreach (RolEntity item in entity)
                {
                    var rol = new RolDTO()
                    {
                        Id = item.Id,
                        Nombre = item.Nombre,
                        Descripcion = item.Descripcion,
                    };
                    lsRols.Add(rol);
                }
            }
            return lsRols;
        }
    }
}
