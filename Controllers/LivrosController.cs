using GestaoBibliotecaAPI.Model;
using GestaoBibliotecaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoBibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly LivroService _livroService;

        public LivrosController(LivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpPost]
        public ActionResult<LivroModel> CriarLivro(LivroModel livro)
        {
            var novoLivro = _livroService.Create(livro);
            return CreatedAtAction(nameof(GetLivro), new { id = novoLivro.LivroId }, novoLivro);
        }

        [HttpGet("{id}")]
        public ActionResult<LivroModel> GetLivro(int id)
        {
            var livro = _livroService.Get(id);
            if (livro == null)
            {
                return NotFound();
            }
            return Ok(livro);
        }

        [HttpGet]
        public ActionResult<IEnumerable<LivroModel>> ListarLivros()
        {
            var livros = _livroService.GetAll();
            return Ok(livros);
        }
    }
}
