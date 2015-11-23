using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Rc.Models;

namespace Rc
{
	public static class InitialData
	{
		const string defaultAdminUserName = "DefaultAdminUserName";
		const string defaultAdminPassword = "DefaultAdminPassword";
		
		public static async Task InitalizeRcDatabaseAsync(IServiceProvider serviceProvider,bool createUsers = true){
			using(var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope()){
				var db = serviceScope.ServiceProvider.GetService<RcContext>();
				
				if(await db.Database.EnsureCreatedAsync()){
					if(createUsers){
						await CreateAdminUser(serviceProvider);
						Console.WriteLine("Create Admin User");
					}
				}
			}
		}
		
		private static async Task CreateAdminUser(IServiceProvider serviceProvider){
			var appEnv = serviceProvider.GetService<IApplicationEnvironment>();
			
			var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
            var configuration = builder.Build();
			
			var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
			
			var user = await userManager.FindByNameAsync(configuration[defaultAdminUserName]);
			if(user == null){
				user = new ApplicationUser{
					UserName = configuration[defaultAdminUserName]
				};
				
				await userManager.CreateAsync(user,configuration[defaultAdminPassword]);
				
				await userManager.AddClaimAsync(user, new Claim("ManageSite","Allowed"));
			}
		}
	}
}