using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Functions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Kennemerland.Services;

namespace Kennemerland.Startup {
	public class Program {
		// DIT IS EEN COMMENT
		public static void Main() {
			IHost host = new HostBuilder()
				.ConfigureFunctionsWorkerDefaults((IFunctionsWorkerApplicationBuilder Builder) => {
					Builder.UseNewtonsoftJson().UseMiddleware<JwtMiddleware>();
				})
				.ConfigureOpenApi()
				.ConfigureServices(Configure)
				.Build();

			host.Run();
		}

		static void Configure(HostBuilderContext Builder, IServiceCollection Services) {
			// Services.AddSingleton<IOpenApiHttpTriggerContext, OpenApiHttpTriggerContext>();
			//Services.AddSingleton<IOpenApiTriggerFunction, OpenApiTriggerFunction>();

			Services.AddSingleton<IUsersService, UserService>();
			Services.AddSingleton<ITokenService, TokenService>();

		}
	}
}


