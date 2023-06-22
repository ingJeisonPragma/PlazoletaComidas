using Food.DataBase;
using Food.DataBase.Repository;
using Food.Domain.Interface.IRepository;
using Food.Domain.Interface.IServices;
using Food.Domain.Interface.IServices.IUserProxy;
using Food.Domain.Services.Services;
using Food.Transversal.Proxy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Food.Domain.Services.Services.UserProxy;
using Food.Domain.Interface.IServices.IMessengerProxy;
using Food.Domain.Services.Services.MessengerProxy;
using Twilio.Clients;

namespace Food.Transversal
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
            services.AddDbContext<FoodDBContext>(cfg =>
            {
                cfg.UseSqlServer(conectionString, x => x.MigrationsHistoryTable("_EFMigrationsHistory", "food")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            //services.AddTransient<IPortalDBContext, PortalDBContext>();
        }

        private static IServiceCollection AddRegisterServices(IServiceCollection services)
        {
            //Va a crear una instancia cada ves que sea requerida
            services.AddTransient<IRestaurantServices, RestaurantServices>();
            services.AddTransient<IDishServices, DishServices>();
            services.AddTransient<IRestaurantEmployeeServices, RestaurantEmployeeServices>();
            services.AddTransient<IOrderServices, OrderServices>();
            services.AddTransient<IEmployeeServices, EmployeeServices>();          

            //Api User
            services.AddTransient<IHttpPetitionServices, HttpPetitionServices>();
            services.AddTransient<IUserProxyServices, UserProxyServices>();
            services.AddTransient<IUserServices, UserServices>();

            //Api Messenger
            services.AddTransient<IMessengerServices, MessengerServices>();
            services.AddTransient<IMessengerProxyServices, MessengerProxyServices>();
            
            return services;
        }

        private static void AddRegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IDishRepository, DishRepository>();
            services.AddScoped<IRestaurantEmployeeRepository, RestaurantEmployeeRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDishRepository, OrderDishRepository>();
        }
    }
}