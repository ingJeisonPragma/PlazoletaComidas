using Food.Domain.Interface.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Food.DataBase
{
    public class FoodDBContext : DbContext
    {
        public FoodDBContext(DbContextOptions<FoodDBContext> option) : base(option)
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

            modelBuilder.HasDefaultSchema("food");

            //List<RolEntity> rols = new();
            //rols.Add(new RolEntity { Nombre = "Administrador", Descripcion = "Administrador.." });
            //rols.Add(new RolEntity { Nombre = "Propietario", Descripcion = "Propietario.." });
            //rols.Add(new RolEntity { Nombre = "Empleado", Descripcion = "Empleado.." });
            //rols.Add(new RolEntity { Nombre = "Cliente", Descripcion = "Cliente.." });

        }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<DishEntity> Dishes { get; set; }
        public DbSet<OrderDishEntity> OrderDishes { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<RestaurantEmployeeEntity> RestaurantEmployees { get; set; }
        public DbSet<RestaurantEntity> Restaurants { get; set; }

    }
}