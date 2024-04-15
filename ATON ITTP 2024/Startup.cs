using ATON_ITTP_2024.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ATON_ITTP_2024_Domain;
using ATON_ITTP_2024.Utilities;

namespace ITTP_2023
{
    public class Startup
    {
        IConfigurationRoot configurationRoot;

        public static IWebHostEnvironment WebHostEnvironment { get; private set; }

        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment configuration)
        {
            configurationRoot = new ConfigurationBuilder().SetBasePath(configuration.ContentRootPath).AddJsonFile("appsettings.json").Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.ConfigureApplicationCookie(configure => configure.Cookie.Expiration = TimeSpan.FromDays(14));

            var connectionString = configurationRoot.GetConnectionString("DefaultConnection");
            services.AddDbContext<ITTP_2024_Context>(opt => opt.UseSqlServer(connectionString));

            services.AddTransient<IUserService, UserService>();

            services.AddControllersWithViews();

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Введите токен авторизации",
                    Name = "Token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Token",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                var basePath = AppContext.BaseDirectory;

                var xmlPath = Path.Combine(basePath, "ATON_ITTP_2024.xml");
                options.IncludeXmlComments(xmlPath);
            });

            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            MigrationHelper.CreateDatabase(builder.UseSqlServer(configurationRoot.GetConnectionString("DefaultConnection")).Options, WebHostEnvironment);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); 
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
               });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}