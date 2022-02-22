using CollageAPI.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollageAPI
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

            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy", builder =>
                {
                    builder.WithOrigins("https://localhost:4200")
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                }
                );
            });

            services.AddTransient<IRoleStore<ApplicationRole>, ApplicationRoleStore>();
            services.AddTransient<UserManager<ApplicationUser>, ApplicationUserManager>();
            services.AddTransient<SignInManager<ApplicationUser>, ApplicationSignInManager>();
            services.AddTransient<RoleManager<ApplicationRole>, ApplicationRoleManager>();
            services.AddTransient<IUserStore<ApplicationUser>, ApplicationUserStore>();
            services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<IdentityApplicationDbContext>()
            .AddUserStore<ApplicationUserStore>()
            .AddUserManager<ApplicationUserManager>()
            .AddRoleManager<ApplicationRoleManager>()
            .AddSignInManager<ApplicationSignInManager>()
            .AddRoleStore<ApplicationRoleStore>()
            .AddDefaultTokenProviders();


            services.AddScoped<ApplicationRoleStore>();
            services.AddScoped<ApplicationUserStore>();




            services.AddEntityFrameworkSqlServer().AddDbContext<IdentityApplicationDbContext>(option =>
option.UseSqlServer(Configuration.GetConnectionString("conStr"),
b => b.MigrationsAssembly("CollageAPI")));

            services.AddControllers();
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CollegeDB")));
            
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Add JWT Authentication

            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);

            var appSetting = appSettingSection.Get<AppSettings>();
            var key = System.Text.Encoding.ASCII.GetBytes(appSetting.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddCookie()
              .AddJwtBearer(x =>
              {
                  x.RequireHttpsMetadata = false;
                  x.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(key),
                      ValidateIssuer = false,
                      ValidateAudience = false
                  };
              });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CollageAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CollageAPI v1"));
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseCors("MyPolicy");

            app.UseAuthorization();

            //IServiceScopeFactory serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            //using (IServiceScope scope = serviceScopeFactory.CreateScope())
            //{
            //    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            //    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //    //Create Admin Role
            //    if (!await roleManager.RoleExistsAsync("Admin"))
            //    {
            //        var role = new ApplicationRole();
            //        role.Name = "Admin";
            //        await roleManager.CreateAsync(role);
            //    }


            //    //Create Employee Role
            //    if (!await roleManager.RoleExistsAsync("Employee"))
            //    {
            //        var role = new ApplicationRole();
            //        role.Name = "Employee";
            //        await roleManager.CreateAsync(role);
            //    }


            //    //Create Admin User

            //    if (await userManager.FindByNameAsync("admin") == null)
            //    {
            //        var user = new ApplicationUser();
            //        user.UserName = "admin";
            //        user.Email = "admin@gmail.com";
            //        var Password = "Admin@123";
            //        var chkuser = await userManager.CreateAsync(user, Password);
            //        if (chkuser.Succeeded)
            //        {
            //            await userManager.AddToRoleAsync(user, "Admin");
            //        }
            //    }

            //    //Create Employee User

            //    if (await userManager.FindByNameAsync("employee") == null)
            //    {
            //        var user = new ApplicationUser();
            //        user.UserName = "employee";
            //        user.Email = "employee@gmail.com";
            //        var Password = "Admin@123";
            //        var chkuser = await userManager.CreateAsync(user, Password);
            //        if (chkuser.Succeeded)
            //        {
            //            await userManager.AddToRoleAsync(user, "Employee");
            //        }
            //    }
            //}
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
