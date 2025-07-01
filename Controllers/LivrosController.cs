using GestaoBibliotecaAPI.Model;
using GestaoBibliotecaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoBibliotecaAPI.Controllers
{
    /// <summary>
    /// Controlador para gerenciar operações relacionadas a livros
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly LivroService _livroService;

        public LivrosController(LivroService livroService)
        {
            _livroService = livroService;
        }

        /// <summary>
        /// Cria um novo livro na biblioteca
        /// </summary>
        /// <param name="livro">Dados do livro a ser criado</param>
        /// <returns>O livro criado com seu ID</returns>
        /// <response code="201">Livro criado com sucesso</response>
        /// <response code="400">Se os dados do livro forem inválidos</response>
        [HttpPost]
        public ActionResult<LivroModel> CriarLivro(LivroModel livro)
        {
            var novoLivro = _livroService.Create(livro);
            return CreatedAtAction(nameof(GetLivro), new { id = novoLivro.LivroId }, novoLivro);
        }

        /// <summary>
        /// Obtém um livro pelo seu ID
        /// </summary>
        /// <param name="id">ID do livro</param>
        /// <returns>O livro encontrado</returns>
        /// <response code="200">Livro encontrado com sucesso</response>
        /// <response code="404">Se o livro não for encontrado</response>
        [HttpGet("{id}")]
        public ActionResult<LivroModel> GetLivro(int id)
        {
            var livro = _livroService.Get(id);
            if (livro == null)
            {
                return NotFound(new { message = "Livro não encontrado" });
            }
            return Ok(livro);
        }

        /// <summary>
        /// Lista todos os livros disponíveis na biblioteca
        /// </summary>
        /// <returns>Lista de todos os livros</returns>
        /// <response code="200">Lista de livros retornada com sucesso</response>
        [HttpGet]
        public ActionResult<IEnumerable<LivroModel>> ListarLivros()
        {
            var livros = _livroService.GetAll();
            if (!livros.Any())
            {
                return Ok(new { message = "Nenhum livro encontrado na biblioteca" });
            }
            return Ok(livros);
        }
    }
}
