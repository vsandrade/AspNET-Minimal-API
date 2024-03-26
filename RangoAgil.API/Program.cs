using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using RangoAgil.API.DbContexts;
using RangoAgil.API.Extensions;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RangoDbContext>(
    o => o.UseSqlite(builder.Configuration["ConnectionStrings:RangoDbConStr"])
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddProblemDetails();

var app = builder.Build();

if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();

    //Referências de Detalhamento que pode ser utilizado.
    //app.UseExceptionHandler(configureApplicationBuider =>
    //{
    //    configureApplicationBuider.Run(
    //        async context =>
    //        {
    //            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    //            context.Response.ContentType = "text/html";
    //            await context.Response.WriteAsync("An unexpected problem happened.");
    //        });
    //});
}

app.RegisterRangosEndpoints();
app.RegisterIngredientesEndpoints();

app.Run();
