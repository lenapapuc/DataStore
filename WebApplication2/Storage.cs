using System.Text.Json.Nodes;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2;

public class Storage : DbContext
{
    public Storage(DbContextOptions<Storage> options) : base(options)
    { }

    public DbSet<Model> Store => Set<Model>();
}