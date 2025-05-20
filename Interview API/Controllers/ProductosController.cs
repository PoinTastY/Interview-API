using Interview_API.Context;
using Interview_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Interview_API.Controllers
{
    [ApiController]
    [Route("/api/productos")]
    public class ProductosController : ControllerBase
    {
        private readonly DbContext _dbContext;


        public ProductosController(ProductosContext productosContext)
        {
            _dbContext = productosContext;
        }

        [HttpGet(Name = "productos")]
        public async Task<ActionResult<List<Producto>>> GetProductos()
        {
            return await _dbContext.Set<Producto>().ToListAsync();
        }

        [HttpPost(Name = "productos")]
        public void AgregarProducto([FromBody] Producto producto)
        {
            _dbContext.Set<Producto>().Add(producto);
            _dbContext.SaveChanges();
        }
    }
}
