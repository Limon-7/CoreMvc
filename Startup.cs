using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentMvc.Models;

namespace StudentMvc
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
            services.AddDbContextPool<StudentMvcContext>(db=>db.UseSqlite(Configuration.GetConnectionString("StudentMvcDB")));

            services.AddIdentity<IdentityUser, IdentityRole>(options=>{
                options.Password.RequiredLength=10;
                options.Password.RequiredUniqueChars=3;
                options.Password.RequireNonAlphanumeric=false;
                options.Password.RequireDigit=false;
                options.Password.RequireUppercase=false;
            }).AddEntityFrameworkStores<StudentMvcContext>();

            services.AddMvc().AddXmlDataContractSerializerFormatters();
            services.AddScoped<IStudentRepository,SqliteStudentRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            //app.UseMvcWithDefaultRoute();
            app.UseAuthentication();
            
            app.UseEndpoints(routes=>{
               routes.MapDefaultControllerRoute();
               //routes.MapControllerRoute("default","{controller=Home}/{action=Index}/{id?}");
            });
          
        }
    }
}
