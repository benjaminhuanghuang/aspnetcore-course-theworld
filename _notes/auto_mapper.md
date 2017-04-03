<PackageReference Include="AutoMapper" Version="6.0.2" />

## Method 1

AutoMapper.Mapper.Initialize(cfg =>
{
    // Map enities to dto models
    // output
    cfg.CreateMap<Author, AuthorDto>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
        $"{src.FirstName} {src.LastName}"))
        .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
        src.DateOfBirth.GetCurrentAge()));

    cfg.CreateMap<Book, BookDto>();
    
    // input
    cfg.CreateMap<AuthorForCreationDto, Author>();
    cfg.CreateMap<BookForCreationDto, Book>();
});


var newTrip = Mapper.Map<Trip>(theTrip);
