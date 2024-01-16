using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RangoAgil.API.Entities;

namespace RangoAgil.API.DbContexts;

public class RangoDbContext(DbContextOptions<RangoDbContext> options) : DbContext(options)
{
    public DbSet<Rango> Rangos { get; set; } = null!;
    public DbSet<Ingrediente> Ingredientes { get; set;} = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<Ingrediente>().HasData(
        new { Id = 1, Nome = "Carne de Vaca" },
        new { Id = 2, Nome = "Cebola" },
        new { Id = 3, Nome = "Cerveja Escura" },
        new { Id = 4, Nome = "Fatia de Pão Integral" },
        new { Id = 5, Nome = "Mostarda" },
        new { Id = 6, Nome = "Chicória" },
        new { Id = 7, Nome = "Maionese" },
        new { Id = 8, Nome = "Vários Temperos" },
        new { Id = 9, Nome = "Mexilhões" },
        new { Id = 10, Nome = "Aipo" },
        new { Id = 11, Nome = "Batatas Fritas" },
        new { Id = 12, Nome = "Tomate" },
        new { Id = 13, Nome = "Extrato de Tomate" },
        new { Id = 14, Nome = "Folha de Louro" },
        new { Id = 15, Nome = "Cenoura" },
        new { Id = 16, Nome = "Alho" },
        new { Id = 17, Nome = "Vinho Tinto" },
        new { Id = 18, Nome = "Leite de Coco" },
        new { Id = 19, Nome = "Gengibre" },
        new { Id = 20, Nome = "Pimenta Malagueta" },
        new { Id = 21, Nome = "Tamarindo" },
        new { Id = 22, Nome = "Peixe Firme" },
        new { Id = 23, Nome = "Pasta de Gengibre e Alho" },
        new { Id = 24, Nome = "Garam Masala" });

        _ = modelBuilder.Entity<Rango>().HasData(
            new { Id = 1, Nome = "Ensopado Flamengo de Carne de Vaca com Chicória" },
            new { Id = 2, Nome = "Mexilhões com Batatas Fritas" },
            new { Id = 3, Nome = "Ragu alla Bolognese" },
            new { Id = 4, Nome = "Rendang" },
            new { Id = 5, Nome = "Masala de Peixe" });

        _ = modelBuilder
            .Entity<Rango>()
            .HasMany(d => d.Ingredientes)
            .WithMany(i => i.Rangos)
            .UsingEntity(e => e.HasData(
                new { RangosId = 1, IngredientesId = 1 },
                new { RangosId = 1, IngredientesId = 2 },
                new { RangosId = 1, IngredientesId = 3 },
                new { RangosId = 1, IngredientesId = 4 },
                new { RangosId = 1, IngredientesId = 5 },
                new { RangosId = 1, IngredientesId = 6 },
                new { RangosId = 1, IngredientesId = 7 },
                new { RangosId = 1, IngredientesId = 8 },
                new { RangosId = 1, IngredientesId = 14 },
                new { RangosId = 2, IngredientesId = 9 },
                new { RangosId = 2, IngredientesId = 19 },
                new { RangosId = 2, IngredientesId = 11 },
                new { RangosId = 2, IngredientesId = 12 },
                new { RangosId = 2, IngredientesId = 13 },
                new { RangosId = 2, IngredientesId = 2 },
                new { RangosId = 2, IngredientesId = 21 },
                new { RangosId = 2, IngredientesId = 8 },
                new { RangosId = 3, IngredientesId = 1 },
                new { RangosId = 3, IngredientesId = 12 },
                new { RangosId = 3, IngredientesId = 17 },
                new { RangosId = 3, IngredientesId = 14 },
                new { RangosId = 3, IngredientesId = 2 },
                new { RangosId = 3, IngredientesId = 16 },
                new { RangosId = 3, IngredientesId = 23 },
                new { RangosId = 3, IngredientesId = 8 },
                new { RangosId = 4, IngredientesId = 1 },
                new { RangosId = 4, IngredientesId = 18 },
                new { RangosId = 4, IngredientesId = 16 },
                new { RangosId = 4, IngredientesId = 20 },
                new { RangosId = 4, IngredientesId = 22 },
                new { RangosId = 4, IngredientesId = 2 },
                new { RangosId = 4, IngredientesId = 21 },
                new { RangosId = 4, IngredientesId = 8 },
                new { RangosId = 5, IngredientesId = 24 },
                new { RangosId = 5, IngredientesId = 10 },
                new { RangosId = 5, IngredientesId = 23 },
                new { RangosId = 5, IngredientesId = 2 },
                new { RangosId = 5, IngredientesId = 12 },
                new { RangosId = 5, IngredientesId = 18 },
                new { RangosId = 5, IngredientesId = 14 },
                new { RangosId = 5, IngredientesId = 20 },
                new { RangosId = 5, IngredientesId = 13 }
            ));

        base.OnModelCreating(modelBuilder);
    }
}
