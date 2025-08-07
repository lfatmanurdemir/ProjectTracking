using ProjectTracking.DAL.Repository;
using ProjectTracking.DAL.Services;

namespace ProjectTracking.Web
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
