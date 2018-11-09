using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vidly.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Vidly.Areas.Identity.Services;
using System.Threading.Tasks;
using System;

namespace Vidly
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<DataContext>(options => options.UseMySql(Configuration.GetConnectionString("Db_Connectionstring")));
            DataContext dataContext = services.BuildServiceProvider().GetRequiredService<DataContext>();

           
            services.AddIdentity<ApplicationUser,IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddGoogle(googleOptions =>
            {

                googleOptions.ClientId = "365168627090-tj86vemmlli7lqrpnie9vkphu1v9djv1.apps.googleusercontent.com";
               
                googleOptions.ClientSecret = "i7j_Gnf5Rz4Ti2LjHgvvs6T7";

            });
            services.AddTransient<IEmailSender, EmailSender>(i =>
                new EmailSender(
                    Configuration["EmailSender:Host"],
                    Configuration.GetValue<int>("EmailSender:Port"),
                    Configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    Configuration["EmailSender:UserName"],
                    Configuration["EmailSender:Password"]
                )
            );
           


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
 private async Task CreateUserRoles(IServiceProvider serviceProvider)  
        {  
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();  
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();  
  
  
            IdentityResult roleResult;  
            //Adding Addmin Role  
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");  
            if (!roleCheck)  
            {  
                //create the roles and seed them to the database  
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));  
            }  
 //Assign Admin role to the main User here we have given our newly loregistered login id for Admin management  
            ApplicationUser user = await UserManager.FindByEmailAsync("ramhziworld@gmail.com");  
            var User = new ApplicationUser();   
            await UserManager.AddToRoleAsync(user, "Admin");   
  
        }  

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IServiceProvider service)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();



            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            CreateUserRoles(service).Wait();
        }
    }
}
