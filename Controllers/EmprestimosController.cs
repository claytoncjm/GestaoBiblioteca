using GestaoBibliotecaAPI.Model;
using GestaoBibliotecaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoBibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmprestimosController : ControllerBase
    {
        private readonly EmprestimoService _emprestimoService;

        public EmprestimosController(EmprestimoService emprestimoService)
        {
            _emprestimoService = emprestimoService;
        }

        [HttpPost("{livroId}")]
        public ActionResult<EmprestimoModel> CriarEmprestimo(int livroId)
        {
            var emprestimo = _emprestimoService.Create(livroId);
            return CreatedAtAction(nameof(GetEmprestimoById), new { id = emprestimo.EmprestimoId }, emprestimo);
        }

        [HttpPut("devolver/{id}")]
        public IActionResult DevolverLivro(int id)
        {
            _emprestimoService.DevolverLivro(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult<EmprestimoModel> GetEmprestimoById(int id)
        {
            var emprestimo = _emprestimoService.GetAll().FirstOrDefault(e => e.EmprestimoId == id);
            if (emprestimo == null)
            {
                return NotFound();
            }
            return Ok(emprestimo);
        }

        [HttpGet]
        public ActionResult<IEnumerable<EmprestimoModel>> ListarEmprestimos()
        {
            var emprestimos = _emprestimoService.GetAll();
            return Ok(emprestimos);
        }
    }
}
