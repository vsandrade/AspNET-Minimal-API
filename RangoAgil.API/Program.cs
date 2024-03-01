using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RangoAgil.API.DbContexts;
using RangoAgil.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RangoDbContext>(
    o => o.UseSqlite(builder.Configuration["ConnectionStrings:RangoDbConStr"])
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/rangos", async Task<Results<NoContent, Ok<IEnumerable<RangoDTO>>>> 
    (RangoDbContext rangoDbContext, 
    IMapper mapper,
    [FromQuery(Name = "name")]string? rangoNome) => 
{

    var rangosEntity = await rangoDbContext.Rangos
                               .Where(x => rangoNome == null || x.Nome.ToLower().Contains(rangoNome.ToLower()))
                               .ToListAsync();
    if (rangosEntity.Count <= 0 || rangosEntity == null)
        return TypedResults.NoContent();
    else
        return TypedResults.Ok(mapper.Map<IEnumerable<RangoDTO>>(rangosEntity));

});

app.MapGet("/rango/{rangoId:int}/ingredientes", async (
    RangoDbContext rangoDbContext, 
    IMapper mapper,
    int rangoId) =>
{
    return mapper.Map<IEnumerable<IngredienteDTO>>((await rangoDbContext.Rangos
                               .Include(rango => rango.Ingredientes)
                               .FirstOrDefaultAsync(rango => rango.Id == rangoId))?.Ingredientes);
});

app.MapGet("/rango/{id:int}", async (
    RangoDbContext rangoDbContext,
    IMapper mapper,
    int id) => 
{

    return mapper.Map<RangoDTO>(await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == id));

});

app.Run();
