## DI
public void ConfigureServices(IServiceCollection services)
{ 
    services.AddLogging();
}


public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                                ILoggerFactory loggerFactory)
{
    loggerFactory.AddConsole(Configuration.GetSection("Logging"));
    // write Debug.Write into log
    loggerFactory.AddDebug();
} 

## Use it
public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
{
    _context = context;
    _logger = logger;
}

public IEnumerable<Trip> GetAllTrips()
{
    _logger.LogInformation("Getting all trips from the database");
    return _context.Trips.ToList();
}