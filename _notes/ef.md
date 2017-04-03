# Dependencies
```xml
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="1.1.1" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="1.1.1" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="1.1.1" />
    ...
    <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="1.0.0" />
```

## Connection String
    

## Create db
 $ dotnet ef migrations add init
 $ dotnet ef database update



## Seed Data
  services.AddTransient<WorldContextSeedData>();  //DI
  
  public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                                ILoggerFactory loggerFactory, WorldContextSeedData seeder)
    {
        ...
        seeder.EnsureSeedData().Wait();
    }