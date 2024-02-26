using Microsoft.AspNetCore.Mvc;
using EntityFramework.Data;
using EntityFramework.Models;

namespace EntityFramework.Controllers
{
    [ApiVersion("1.0")]
    [Route("[controller]")]
    [ApiController]
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Categoria> lista = _context.Categoria;
            return Ok(lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Categoria categoria)
        {
            if(ModelState.IsValid)
            {
                _context.Categoria.Add(categoria);
                _context.SaveChanges();
                return Ok("created category");
            }

            return StatusCode(500, "cannot create category");
        }
    }
}
