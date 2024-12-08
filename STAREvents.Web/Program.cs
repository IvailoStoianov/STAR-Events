using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STAREvents.Data.Models;
using STAREvents.Services;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Services.Mapping;
using STAREvents.Web.Data;
using STAREvents.Web.Infrastructure.Extensions;
using STAREvents.Web.Models;
using STAREvents.Data;
using Azure.Storage.Blobs;
using STAREvents.Services.Data;
using System.Configuration;
using Microsoft.AspNetCore.Http.Features;

namespace STAREvents.Web
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Should remove later
            builder.Logging.ClearProviders(); 
            builder.Logging.AddConsole();

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<STAREventsDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole<Guid>>(cfg =>
                {
                    ConfigureIdentity(builder, cfg);
                })
                .AddEntityFrameworkStores<STAREventsDbContext>()
                .AddRoles<IdentityRole<Guid>>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddDefaultTokenProviders();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 104857600; // 100 MB
            });


            builder.Services.RegisterRepositories(typeof(ApplicationUser).Assembly);
            builder.Services.RegisterUserDefinedServices(typeof(IBaseService).Assembly);

            builder.Services.AddHostedService<EventCleanupService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).Assembly);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "Areas",
                pattern: "{area:exists}/{controller=Admin}/{action=Dashboard}/{id?}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.UseStatusCodePagesWithRedirects("/Error/{0}");

            // Initialize roles and admin user
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                var configuration = services.GetRequiredService<IConfiguration>();
                await RoleInitializer.InitializeAsync(userManager, roleManager, configuration);
            }

            app.Run();
        }

        private static void ConfigureIdentity(WebApplicationBuilder builder, IdentityOptions cfg)
        {
            cfg.Password.RequireDigit =
                builder.Configuration.GetValue<bool>("Identity:Password:RequireDigits");
            cfg.Password.RequireLowercase =
                builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
            cfg.Password.RequireUppercase =
                builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
            cfg.Password.RequireNonAlphanumeric =
                builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumerical");
            cfg.Password.RequiredLength =
                builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
            cfg.Password.RequiredUniqueChars =
                builder.Configuration.GetValue<int>("Identity:Password:RequiredUniqueCharacters");

            cfg.SignIn.RequireConfirmedAccount =
                builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
            cfg.SignIn.RequireConfirmedEmail =
                builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");
            cfg.SignIn.RequireConfirmedPhoneNumber =
                builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedPhoneNumber");

            cfg.User.RequireUniqueEmail =
                builder.Configuration.GetValue<bool>("Identity:User:RequireUniqueEmail");
        }
    }
}
