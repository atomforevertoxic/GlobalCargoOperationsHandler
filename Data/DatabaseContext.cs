using CargoTransactionDatabase.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTransactionDatabase.Data
{
  public class DatabaseContext: DbContext
  {
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<Good> Goods { get; set; } = null!;
    public DbSet<Ship> Ships { get; set; } = null!;
    public DbSet<TransportationDetail> Details { get; set; } = null!;

    /// <summary>
    /// Метод обеспечивает связь между базой данных SQL и C# проектом
    /// </summary>
    /// <param name="optionsBuilder">Строка подключения к базе данных</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer("Server =ATOM\\SQLEXPRESS; Initial Catalog = TransportationDb; Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True");
    }
  }
}
