##
AppController

// For single request
services.AddScoped<IMailService, DebugMailService>();

//
services.AddSingleton<IMailService, DebugMailService>();

// Only when needed 
services.AddTransient<IMailService, DebugMailService>();