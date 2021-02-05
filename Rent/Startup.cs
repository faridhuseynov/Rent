using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Rent.DomainModels.Models;
using Rent.Repositories;
using Rent.ServiceLayers;
using Microsoft.AspNetCore.Identity;
using Rent.ServiceLayers.Hubs;

namespace Rent
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
            var connString = Configuration.GetConnectionString("DefaultConnection");
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connString);
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            //services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IProposalsRepository, ProposalsRepository>();
            services.AddScoped<IProposalsService, ProposalsService>();
            services.AddScoped<IProposalTypesRepository, ProposalTypesRepository>();
            services.AddScoped<IImagesRepository, ImagesRepository>();
            services.AddScoped<IWishListProdsRepository, WishListProdsRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IRatingsRepository, RatingsRepository>();
            services.AddScoped<ISubcategoriesRepository, SubcategoriesRepository>();
            services.AddScoped<ISubcategoriesService, SubcategoriesService>();
            //services.AddScoped(IUsersRepository, UsersRepository);
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            });
            services.AddSession();
            services.AddSignalR();
            services.AddAuthentication()
                .AddGoogle(options => {
                options.ClientId = "676285896082-juvia97abjs74vlsb5qet9akdo7a15vb.apps.googleusercontent.com";
                options.ClientSecret = "ALjUjMKB327-ud6MRCM3_zEg";
            })
                .AddFacebook(options => {
                    options.AppId = "1615373298662814";
                    options.AppSecret = "8e648210da50c6516c8371d13f54a7a5";
            });


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
                //app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                        name: "areas",
                        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapHub<HubService>("/home/messages/inbox");
                endpoints.MapHub<ChatServiceHub>("/chatHub");
                endpoints.MapHub<FavoriteCountServiceHub>("/wishlistCountHub");

            });

            RentDbInitializer.SeedData(app).Wait();
        }
    }
}
