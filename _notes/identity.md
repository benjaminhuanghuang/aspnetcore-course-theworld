##
   <PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="1.1.1"/>


## DbContext
 public class WorldContext : IdentityDbContext<WorldUser> //DbContext    

## User 
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TheWorld.Models
{
    public class WorldUser:IdentityUser
    {
        
    }
}

## Migration
    $ dotnet ef migrations add adding_identity
    $ dotnet ef database update

## Seed Data
public WorldContextSeedData(WorldContext context, UserManager<WorldUser> userManager)

## Config
    services.AddIdentity<WorldUser, IdentityRole>(config =>
    {
        config.User.RequireUniqueEmail = true;
        config.Password.RequiredLength = 4;
        config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
    }).AddEntityFrameworkStores<WorldContext>();


    app.UseIdentity();

## Login

    public AuthController(SignInManager<WorldUser> signInManager)
    {
        _signInManager = signInManager;
    }