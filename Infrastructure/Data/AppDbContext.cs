using Microsoft.EntityFrameworkCore;
using Api.Domain.Models;

namespace Api.Infrastructure.Data;

public class AppDbContext : DbContext{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Name)
            .IsUnique(); //Garante que o nome do usuario seja unico
    }

    public DbSet<User> Users { get; set; } //Nova tabela do banco de dados


}

//Configuração do banco de dados