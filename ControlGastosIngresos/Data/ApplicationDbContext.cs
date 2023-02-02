using ControlGastosIngresos.Controllers;
using ControlGastosIngresos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlGastosIngresos.Data
{
    public class ApplicationDbContext : DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
               
        }
        public DbSet<Categoria> Categorias { get; set; }
       // public DbSet<ControlGastosIngresos.Models.GastoIngreso> GastoIngreso { get; set; }
        public DbSet<GastoIngreso> GastoIngresos { get; set; }
    }
}
