using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Rc.Models;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Rc
{
	public class Startup
	{
		private readonly Platform _platform;
		
		public Startup(IApplicationEnvironment appEnv,IRuntimeEnvironment runtimeEnvironment){
			var builder = new ConfigurationBuilder()
				.SetBasePath(appEnv.ApplicationBasePath)
				.AddJsonFile("config.json")
				.AddEnvironmentVariables();
				
			Configuration = builder.Build();
			
			_platform = new Platform(runtimeEnvironment);
		}
		
		public IConfiguration Configuration { get; private set; }
		
		public void ConfigureServices(IServiceCollection services){
			
			services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
			
			services.AddEntityFramework()
				.AddSqlite()
				.AddDbContext<RcContext>(options=>{
					options.UseSqlite(Configuration["Data:ConnectionString"]);
				});
				
			services.AddIdentity<ApplicationUser,IdentityRole>(options=>{
					options.Cookies.ApplicationCookie.AccessDeniedPath = "/Home/AccessDenied";
				})
				.AddEntityFrameworkStores<RcContext>()
				.AddDefaultTokenProviders();
				
			
			services.AddMvc();
			
			services.AddCaching();
			
			services.AddSession();
			
			services.AddAuthorization(options=>{
				options.AddPolicy(
					"ManageSite",
					authBuilder=>{
						authBuilder.RequireClaim("ManageSite","Allowed");
					});
			});
			
			new Data.DataInit().RegisterAll(services);
		}
		
		public void ConfigureDevelopment(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(minLevel: LogLevel.Error);

            // StatusCode pages to gracefully handle status codes 400-599.
            app.UseStatusCodePagesWithRedirects("~/Home/StatusCodePage");

            // Display custom error page in production when error occurs
            // During development use the ErrorPage middleware to display error information in the browser
            app.UseDeveloperExceptionPage();

            app.UseDatabaseErrorPage();

            // Add the runtime information page that can be used by developers
            // to see what packages are used by the application
            // default path is: /runtimeinfo
            app.UseRuntimeInfoPage();

            Configure(app);
        }
		
		public void Configure(IApplicationBuilder app){
			
			app.UseDatabaseErrorPage();
			
			app.UseRuntimeInfoPage();
			
			app.UseWelcomePage("/welcome");
			
			app.UseSession();
			
			app.UseStaticFiles();
			
			app.UseIdentity();
			
			app.UseMvc(routes=>{
				routes.MapRoute(
					name: "areaRoute",
					template: "{area:exists}/{controller}/{action}",
					defaults: new { action = "Index" });
				
				routes.MapRoute(
					name:"default",
					template:"{controller}/{action}/{id?}",
					defaults:new {controller="Home",action = "Index"}
				);
				
				routes.MapRoute(
                    name: "api",
                    template: "api/{controller}/{id?}");
			});
			
			InitialData.InitalizeRcDatabaseAsync(app.ApplicationServices).Wait();
		}
	}	
}