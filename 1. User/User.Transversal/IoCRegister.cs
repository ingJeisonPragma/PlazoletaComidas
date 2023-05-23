using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.DataBase;
using User.DataBase.Repository;
using User.Domain.Business.DTO.FoodProxyDTO;
using User.Domain.Interface.IRepository;
using User.Domain.Interface.IServices;
using User.Domain.Interface.IServices.IFoodProxy;
using User.Domain.Services.Services;
using User.Domain.Services.Services.FoodProxy;
using User.Transversal.Proxy;

namespace User.Transversal
{
    public static class IoCRegister
    {
        public static IServiceCollection AddRegistration(this IServiceCollection services, string conectionString = "")
        {
            AddRegisterDBContext(services, conectionString);
            AddRegisterRepositories(services);
            AddRegisterServices(services);
            return services;
        }

        private static void AddRegisterDBContext(this IServiceCollection services, string conectionString)
        {
            services.AddDbContext<UserDBContext>(cfg =>
            {
                cfg.UseSqlServer(conectionString, x => x.MigrationsHistoryTable("_EFMigrationsHistory", "usr")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            //services.AddTransient<IPortalDBContext, PortalDBContext>();
        }

        private static IServiceCollection AddRegisterServices(IServiceCollection services)
        {
            //Va a crear una instancia cada ves que sea requerida
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<ILoginServices, LoginServices>();


            //Api Food
            services.AddTransient<IHttpPetitionServices, HttpPetitionServices>();
            services.AddTransient<IFoodProxyServices, FoodProxyServices>();
            services.AddTransient<IFoodServices, FoodServices>();

            return services;
        }

        private static void AddRegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, userRepository>();


        }
    }
}
