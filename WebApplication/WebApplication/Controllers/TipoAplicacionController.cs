using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class TipoAplicacionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoAplicacionController(ApplicationDbContext context)
        {
                _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<TipoAplicacion> tipoAplicaciones = _context.TipoAplicacion;
            return View(tipoAplicaciones);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(TipoAplicacion tipoAplicacion)
        {
            _context.TipoAplicacion.Add(tipoAplicacion);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
