using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STAREvents.Data.Models;
using STAREvents.Services.Data;
using STAREvents.Services.Data.Interfaces;
using STAREvents.Web.Data;
using STAREvents.Web.Infrastructure.Extensions;
using STAREvents.Web.Models;
using static STAREvents.Common.EntityValidationConstants.ConfigurationConstants;
using static STAREvents.Web.Infrastructure.Extensions.IdentityConfiguration;

namespace STAREvents.Web
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add<CustomExFilter>(); 
            });
            builder.Services.AddRazorPages();

            builder.Services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = MultipartBodyLengthLimitConstant; 
            });


            builder.Services.RegisterRepositories(typeof(ApplicationUser).Assembly);
            builder.Services.RegisterUserDefinedServices(typeof(IBaseService).Assembly);

            builder.Services.AddHostedService<EventCleanupService>();


            var app = builder.Build();

            app.Use(async (context, next) =>
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Handling request: {RequestPath}", context.Request.Path);
                await next.Invoke();
                logger.LogInformation("Finished handling request.");
            });

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
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
    }
}
