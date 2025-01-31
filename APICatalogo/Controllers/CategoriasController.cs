using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {

        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                return _context.Categorias.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no Servidor!");
            }
            
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            try
            {
                return _context.Categorias.Include(c => c.Produtos).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no Servidor!");
            }

        }

        [HttpGet("{id:int}", Name ="ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            try
            {
                var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(c => c.CategoriaId == id);
                if (categoria is null) return NotFound("Categoria não encontrada");
                return categoria;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no Servidor!");
            }
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            try
            {
                if(categoria is null) return BadRequest();

                _context.Add(categoria);
                _context.SaveChanges();
                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no Servidor!");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            try
            {
                if(id != categoria.CategoriaId) return BadRequest();

                _context.Entry(categoria).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no Servidor!");
            }
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

                if (categoria is null) return NotFound("Categoria não encontrada");

                _context.Categorias.Remove(categoria);
                _context.SaveChanges();
                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no Servidor!");
            }
        }
    }
}
