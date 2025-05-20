using Interview_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Interview_API.Context
{
    public class ProductosContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; } = null!;

        public ProductosContext(DbContextOptions<ProductosContext> options) : base(options)
        {
        }



    }
}
