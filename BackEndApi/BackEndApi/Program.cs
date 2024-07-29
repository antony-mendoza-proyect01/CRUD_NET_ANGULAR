using BackEndApi.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;

using BackEndApi.Services.Contrato;
using BackEndApi.Services.implementacion;

using AutoMapper;
using BackEndApi.DTO;
using BackEndApi.Utilidades;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Server = (local); DataBase = CRUD_PELICULA; Trusted_Connection = True; TrustServerCertificate = True;
builder.Services.AddDbContext<CrudPeliculaContext>(options =>
{
    //obtenemos la cadena de conexion
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

//inyectamos los la interfaz y los servicios
builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<IPeliculaService, PeliculaService>();

//inyectamos los mapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//cors 
builder.Services.AddCors(options =>
{
    options.AddPolicy("PoliticasCors", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader() 
        .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


#region Peticiones API REST

app.MapGet("/genero/get", async (
    //inyectamos el mapper y el servicio
    IGeneroService _generoService,
    IMapper _mapper
    ) =>
{
    var listaGenero = await _generoService.GetList();
    var listaGeneroDTO = _mapper.Map<List<GeneroDTO>>(listaGenero);

    if (listaGeneroDTO.Count >0)
       return Results.Ok(listaGeneroDTO);
    else 
        return Results.NotFound();
});


app.MapGet("/pelicula/get", async (
    //inyectamos el mapper y el servicio
    IPeliculaService _peliculaService,
    IMapper _mapper
    ) =>
{
    var listaPelicula = await _peliculaService.GetList();
    var listaPeliculaDTO = _mapper.Map<List<PeliculaDTO>>(listaPelicula);

    if (listaPeliculaDTO.Count > 0)
        return Results.Ok(listaPeliculaDTO);
    else
        return Results.NotFound();
});

app.MapPost("/pelicula/post", async (
    PeliculaDTO modelo,
    IPeliculaService _peliculaService,
     IMapper _mapper
    ) =>{

    var _pelicula = _mapper.Map<Pelicula>(modelo);
    var _peliculaCreada = await _peliculaService.Add(_pelicula);

    if(_peliculaCreada.IdPelicula !=0)
        return Results.Ok(_mapper.Map<PeliculaDTO>(_peliculaCreada));
    else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
});
app.MapPut("/pelicula/put/{idPelicula}", async (
    int idPelicula,
    PeliculaDTO modelo,
    IPeliculaService _peliculaService,
     IMapper _mapper) => {
         //validamos que haya pelicula
         var encontrado = await _peliculaService.Get(idPelicula);

         if(encontrado is null)
             return Results.NotFound();
         var _pelicula = _mapper.Map<Pelicula>(modelo);

         encontrado.NombrePelicula = _pelicula.NombrePelicula;
         encontrado.IdGenero = _pelicula.IdGenero;

         var respuesta = await _peliculaService.Update(encontrado);

         if(respuesta)
             return Results.Ok(_mapper.Map<PeliculaDTO>(encontrado));
         else
             return Results.StatusCode(StatusCodes.Status500InternalServerError);

     });
app.MapDelete("/pelicula/delete/{idPelicula}", async (
    int idPelicula,
       IPeliculaService _peliculaService
    ) => {
        //validamos que haya pelicula
        var encontrado = await _peliculaService.Get(idPelicula);

        if (encontrado is null)
            return Results.NotFound();

        var respuesta = await _peliculaService.Delete(encontrado);

        if (respuesta)
            return Results.Ok();
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);

    });

#endregion

app.UseCors("PoliticasCors");
app.Run();

