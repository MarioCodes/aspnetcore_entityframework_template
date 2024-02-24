using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Categoria> lista = _context.Categoria;
            return View(lista);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Categoria categoria)
        {
            if(ModelState.IsValid)
            {
                _context.Categoria.Add(categoria);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(categoria);
        }
    }
}
