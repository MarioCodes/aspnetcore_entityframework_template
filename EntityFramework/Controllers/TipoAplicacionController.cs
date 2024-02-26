using Microsoft.AspNetCore.Mvc;
using EntityFramework.Data;
using EntityFramework.Models;

namespace EntityFramework.Controllers
{
    [ApiVersion("1.0")]
    [Route("[controller]")]
    [ApiController]
    public class TipoAplicacionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoAplicacionController(ApplicationDbContext context)
        {
                _context = context;
        }

        [HttpPost]
        public IActionResult Crear(TipoAplicacion tipoAplicacion)
        {
            _context.TipoAplicacion.Add(tipoAplicacion);
            _context.SaveChanges();
            return Ok("created category");
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<TipoAplicacion> tipos = _context.TipoAplicacion;
            return Ok(tipos);
        }
    }
}
