using Microsoft.EntityFrameworkCore;
using Schronisko.Models;

namespace Schronisko.Models.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<Animal> Animals => Set<Animal>();
    public DbSet<Visit> Visits => Set<Visit>();
}