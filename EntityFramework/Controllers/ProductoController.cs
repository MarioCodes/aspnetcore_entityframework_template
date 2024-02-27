using EntityFramework.Data;
using EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Producto> lista = _context.Producto
                                            .Include(categoria => categoria.categoria)
                                            .Include(tipo => tipo.tipoAplicacion);
            return Ok(lista);
        }

        [HttpPost("/upsert")]
        public IActionResult Upsert(Producto producto)
        {
            var productoExistente = _context.Producto
                .Include(categoria => categoria.categoria)
                .Include(tipo => tipo.tipoAplicacion)
                .FirstOrDefault(p => p.Id == producto.Id);

            if(productoExistente == null)
            {
                // Create
                _context.Producto.Add(producto);
                _context.SaveChanges();
                return Ok("producto created");
            } else
            {
                // Update
                _context.Producto.Update(producto);
                _context.SaveChanges();
                return Ok("producto updated");
            }
        }
    }
}
