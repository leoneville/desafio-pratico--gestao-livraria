using GestaoLivraria.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoLivraria.Data;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=database.sqlite");
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        base.OnConfiguring(optionsBuilder);
    }
}
