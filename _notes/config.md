 ## In Startup
 var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

## In Controller

public AppController(IMailService mailService, IConfigurationRoot config)
        {   
            _mailService = mailService;
            _config = config;
        }

var to = _config["MailSettings:ToAddress"];