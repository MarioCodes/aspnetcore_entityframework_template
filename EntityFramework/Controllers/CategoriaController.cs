using Microsoft.AspNetCore.Mvc;
using EntityFramework.Data;
using EntityFramework.Models;

namespace EntityFramework.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Categoria> lista = _context.Categoria;
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Categoria? obj = _context.Categoria.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return Ok(obj);
        }

        [HttpPost("create")]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Categoria.Add(categoria);
                _context.SaveChanges();
                return Ok("created category");
            }

            return StatusCode(500, "cannot create category");
        }

        [HttpPost("update")]
        public IActionResult Update(Categoria categoria)
        {
            if(ModelState.IsValid)
            {
                _context.Categoria.Update(categoria);
                _context.SaveChanges();
                return Ok("updated category");
            }

            return StatusCode(500, "cannot update category");
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Categoria? obj = _context.Categoria.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _context.Categoria.Remove(obj);
            _context.SaveChanges();
            return Ok("category deleted");
        }

    }
}
