using Dynamicweb.Host.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CustomWebApi
{
	public sealed class CustomWebApiPipeline : IPipeline, IWebApi
	{
		public int Rank { get; } = 10;
		public void RegisterApplicationComponents(IApplicationBuilder app)
		{
			AddApi(app);
		}
		private static void AddApi(IApplicationBuilder appBuilder)
		{
			appBuilder.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/custom", StringComparison.OrdinalIgnoreCase), app =>
			{
				app.UseRouting();
				app.UseEndpoints(endpoints =>
				{
					endpoints.MapControllers();
				});
			});
		}
		public void RegisterServices(IServiceCollection services, IMvcCoreBuilder mvcBuilder)
		{
			services.AddControllers();
		}
		public void RunInitializers()
		{

		}
	}
}
