using Elearning.API;
using Elearning.Entittes;
using Elearning.Entittes.DbContexts;
using Elearning.Entittes.Models;

using Microsoft.AspNetCore.Identity;

using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

builder.Host.UseNLog();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ElearningContext>();
     DataSeeder.SeedData(context);
    var services = scope.ServiceProvider;
   // var loggerFactory = services.GetRequiredService<ILoggerProvider>();
    //var logger = loggerFactory.CreateLogger("app");
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await Elearning.API.Seeds.DefaultRoles.SeedAsync(roleManager);
    await Elearning.API.Seeds.DefaultUsers.SeedBasicUserAsync(userManager);
    await Elearning.API.Seeds.DefaultUsers.SeedSuperAdminUserAsync(userManager, roleManager);
}
//app.Run();
var host = app;
//using (var scope = host.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
//    try
//    {
//      //  var context = services.GetRequiredService<ApplicationDbContext>();
//        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
//        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
//     //   await ContextSeed.SeedRolesAsync(userManager, roleManager);
//    }
//    catch (Exception ex)
//    {
//        var logger = loggerFactory.CreateLogger<Program>();
//        logger.Log(ex, "An error occurred seeding the DB.");
//    }
//}
//host.Run();




startup.Configure(app, builder.Environment);

