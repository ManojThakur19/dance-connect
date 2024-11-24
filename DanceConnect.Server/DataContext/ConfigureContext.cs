using Microsoft.EntityFrameworkCore;

namespace DanceConnect.Server.DataContext
{
    public static class ConfigureContext
    {
        public static void AddConfigureContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<DanceConnectContext>(options => options.UseSqlServer(connectionString, optionsBuilder => optionsBuilder.MigrationsAssembly("DanceConnect.Server")));
            //services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            //services.AddTransient<IUnitOfWork, UnitOfWork>();

            //Service Registration

            //Repository Registration
        }
    }
}
