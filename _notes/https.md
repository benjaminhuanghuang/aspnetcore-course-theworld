## ConfigureServices
services.AddMvc(config =>{
    //redirect request to https
    config.Filters.Add(new RequireHttpsAttribute());
})