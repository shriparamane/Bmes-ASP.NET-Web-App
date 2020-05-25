using Bmes.Database;
using Bmes.Repositories;
using Bmes.Repositories.Implementations;
using Bmes.Services;
using Bmes.Services.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Bmes
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
            services.AddControllersWithViews();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddMemoryCache();
            services.AddSession();
            services.AddDbContext<BmesDbContext>(options => options.UseSqlite(Configuration["Data:BmesWebApp:ConnectionString"]));

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICartRepository, CartRepository>();
            services.AddTransient<ICartItemRepository, CartItemRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient <ICustomerRepository, CustomerRepository> ();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderItemRepository, OrderItemRepository>();

            services.AddTransient<ICatalogueService, CatalogueService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<ICheckoutService, CheckoutService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "catalogue/{category_slug}/{brand_slug}/page{page:int}",
                    defaults: new { controller = "Catalogue", action = "Index" }
                );
                routes.MapRoute(
                    name: null,
                    template: "page{page:int}",
                    defaults: new
                    {
                        controller = "Catalogue",
                        action = "Index"
                    }
                );
                routes.MapRoute(
                    name: null,
                    template: "catalogue/{category_slug}/{brand_slug}",
                    defaults: new
                    {
                        controller = "Catalogue",
                        action = "Index"
                    }
                );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Catalogue}/{action=Index}/{id?}");
            });
        }
    }
}
