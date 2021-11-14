using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ShopBridge.BusinessLogic;
using ShopBridge.Database;
using ShopBridge.Middleware;
using ShopBridge.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ShopBridge
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        }

        /// <summary>
        /// AssemblyName
        /// </summary>
        public string AssemblyName { get; }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ConfigureServices
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<ShopBridgeContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ShopBridgeCS"), options => options.EnableRetryOnFailure(
                     Configuration.GetValue<int>("DBRetryCount")));
            });
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductShip-API", Version = "v1" });
                options.IncludeXmlComments($"{AssemblyName}.xml");
            });
            services.AddScoped<DbContext, ShopBridgeContext>();
            services.AddScoped<IInventoryBusinessLogic, InventoryBusinessLogic>();
            services.AddScoped<ISupplierBussinessLogic, SupplierBussinessLogic>();
            services.AddScoped<ICategoryBusinessLogic, CategoryBusinessLogic>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="context"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ShopBridgeContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Custom Error Handler Middleware to glabally handles all type of errors.
            //Middleware Invoke Function Will wait for every Http requests
            //and catch exception accour in below middlewares
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // migrate database changes on startup (includes initial db creation)
            context.Database.Migrate();
            context.Initialize();
            app.UseSwagger();
            app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductShip API"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
