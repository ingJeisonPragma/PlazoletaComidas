using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Interface.Entities;

namespace User.DataBase
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> option) : base(option)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                SqlServerOptionsExtension CnxOptios = (SqlServerOptionsExtension)optionsBuilder.Options.Extensions.OfType<SqlServerOptionsExtension>().First();
                string cnx = CnxOptios.ConnectionString;

                if (cnx != null)
                    optionsBuilder.UseSqlServer(cnx).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("usr");

            //List<RolEntity> rols = new();
            //rols.Add(new RolEntity { Nombre = "Administrador", Descripcion = "Administrador.." });
            //rols.Add(new RolEntity { Nombre = "Propietario", Descripcion = "Propietario.." });
            //rols.Add(new RolEntity { Nombre = "Empleado", Descripcion = "Empleado.." });
            //rols.Add(new RolEntity { Nombre = "Cliente", Descripcion = "Cliente.." });

        }

        public DbSet<RolEntity> Rols { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        
    }
}
