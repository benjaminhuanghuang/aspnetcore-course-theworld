## DI
public void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<IWorldRepository, WorldRepository>();
    //-- For testing
    // services.AddScoped<IWorldRepository, MockRepository>();
}

public AppController(IWorldRepository repository)
{
    _repository = repository;
}
       