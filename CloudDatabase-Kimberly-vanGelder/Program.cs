using DAL;
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Functions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using Repository.Interfaces;
using Service;
using Service.Interfaces;
using System.Threading.Tasks;

namespace CloudDatabase_Kimberly_vanGelder
{
    public class Program
    {
		public static void Main()
		{
			IHost host = new HostBuilder()
				.ConfigureFunctionsWorkerDefaults(worker => worker.UseNewtonsoftJson())
				.ConfigureServices(Configure)
				.Build();

			host.Run();
		}

		static void Configure(HostBuilderContext Builder, IServiceCollection Services)
		{
			Services.AddDbContext<UserContext>(options =>
			options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=UserInfo;Trusted_Connection=True;MultipleActiveResultSets=true"));
			Services.AddSingleton<IOpenApiHttpTriggerContext, OpenApiHttpTriggerContext>();
			Services.AddSingleton<IOpenApiTriggerFunction, OpenApiTriggerFunction>();
			Services.AddSingleton<IUserService, UserService>();
			Services.AddTransient<IUserRepository, UserRepository>();

		}
	}
}