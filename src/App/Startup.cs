using Domain.Context;
using Domain.Interfaces;
using Infrastructure.Services;

namespace App;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // This method gets called by the runtime. 
    // Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        string connectionString =
            Configuration["ConnectionString"] ?? Configuration.GetConnectionString("ConnectionString") ??
            "mongodb://sheymor:password@localhost:27017";
        services.AddScoped<DatabaseContext>(config => new DatabaseContext(connectionString!));
        services.AddScoped<ITrainerServices, TrainerServices>();
        services.AddScoped<IPokemonServices, PokemonServices>();
        services.AddMemoryCache();
    }
}