using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using StoreAPI.Data;
using StoreAPI.Data.Repositories;
using StoreAPI.Models;
using System.Text;

namespace StoreAPI
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
            //JSON error
            services.AddControllers().AddNewtonsoftJson(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //Database
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            //Swagger
            //services.AddSwaggerDocument();
            services.AddOpenApiDocument(c => 
            { 
                c.DocumentName = "apidocs"; 
                c.Title = "Store API"; 
                c.Version = "v1";
                c.Description = "The API for the Sneaker Store website."; 
                c.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT Token", new OpenApiSecurityScheme 
                { 
                    Type = OpenApiSecuritySchemeType.ApiKey, 
                    Name = "Authorization", 
                    In = OpenApiSecurityApiKeyLocation.Header, 
                    Description = "Copy 'Bearer' + valid JWT token into field" 
                }));
                c.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
            });
            
            //DatabaseInitializer & Repositories
            services.AddScoped<DataInitializer>();
            services.AddScoped<IRepository<Brand>, BrandRepository>();
            services.AddScoped<IRepository<Customer>, CustomerRepository>();
            services.AddScoped<IRepository<Sneaker>, SneakerRepository>();
            services.AddScoped<IRepository<Ask>, AskRepository>();
            services.AddScoped<IRepository<Bid>, BidRepository>();

            //Authorization
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddAuthentication(x => { x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false; x.SaveToken = true; x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true//Ensure token hasn't expired
                };
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataInitializer dataInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //Swagger
            app.UseOpenApi();

            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            dataInitializer.InitializeData().Wait();

            //Authentication
            app.UseAuthentication();
        }
    }
}
