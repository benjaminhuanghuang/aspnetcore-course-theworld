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

## UI
@if (User.Identity.IsAuthenticated)
{
    <img src="~/img/user1.jpg" alt="headshot" class="headshot" />
    <span id="username">@User.Identity.Name</span>
}

## Use identity in API

<PackageReference Include="Microsoft.AspNetCore.Authentication.Cookies" Version="1.1.1" />

[RouteAttribute("api/trips")]
[Authorize]
public class TripsController : Controller

[Authorize] will return login page when call API
 
 Fix it by adding:
 config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                {