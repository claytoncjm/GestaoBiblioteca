using GestaoBibliotecaAPI.Model;
using GestaoBibliotecaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoBibliotecaAPI.Controllers
{
    /// <summary>
    /// Controlador para gerenciar operações relacionadas a empréstimos
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EmprestimosController : ControllerBase
    {
        private readonly EmprestimoService _emprestimoService;

        public EmprestimosController(EmprestimoService emprestimoService)
        {
            _emprestimoService = emprestimoService;
        }

        /// <summary>
        /// Solicita um novo empréstimo de um livro
        /// </summary>
        /// <param name="livroId">ID do livro a ser emprestado</param>
        /// <returns>O empréstimo criado</returns>
        /// <response code="201">Empréstimo criado com sucesso</response>
        /// <response code="400">Se o livro não existir ou não estiver disponível</response>
        [HttpPost("{livroId}")]
        public ActionResult<EmprestimoModel> CriarEmprestimo(int livroId)
        {
            var emprestimo = _emprestimoService.Create(livroId);
            return CreatedAtAction(nameof(GetEmprestimoById), new { id = emprestimo.EmprestimoId }, emprestimo);
        }

        /// <summary>
        /// Devolve um livro emprestado
        /// </summary>
        /// <param name="id">ID do empréstimo a ser devolvido</param>
        /// <response code="204">Livro devolvido com sucesso</response>
        /// <response code="404">Se o empréstimo não for encontrado</response>
        /// <response code="400">Se o empréstimo já tiver sido devolvido</response>
        [HttpPut("devolver/{id}")]
        public IActionResult DevolverLivro(int id)
        {
            _emprestimoService.DevolverLivro(id);
            return NoContent();
        }

        /// <summary>
        /// Obtém um empréstimo específico pelo seu ID
        /// </summary>
        /// <param name="id">ID do empréstimo</param>
        /// <returns>O empréstimo encontrado</returns>
        /// <response code="200">Empréstimo encontrado com sucesso</response>
        /// <response code="404">Se o empréstimo não for encontrado</response>
        [HttpGet("{id}")]
        public ActionResult<EmprestimoModel> GetEmprestimoById(int id)
        {
            var emprestimo = _emprestimoService.GetAll().FirstOrDefault(e => e.EmprestimoId == id);
            if (emprestimo == null)
            {
                return NotFound(new { message = "Empréstimo não encontrado" });
            }
            return Ok(emprestimo);
        }

        /// <summary>
        /// Lista todos os empréstimos
        /// </summary>
        /// <returns>Lista de todos os empréstimos</returns>
        /// <response code="200">Lista de empréstimos retornada com sucesso</response>
        [HttpGet]
        public ActionResult<IEnumerable<EmprestimoModel>> ListarEmprestimos()
        {
            var emprestimos = _emprestimoService.GetAll();
            if (!emprestimos.Any())
            {
                return Ok(new { message = "Nenhum empréstimo encontrado" });
            }
            return Ok(emprestimos);
        }
    }
}
