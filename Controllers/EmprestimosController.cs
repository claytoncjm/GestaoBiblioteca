using GestaoBibliotecaAPI.Model;
using GestaoBibliotecaAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public ActionResult<EmprestimoModel> SolicitarEmprestimo(int livroId)
        {
            var emprestimo = _emprestimoService.Create(livroId);
            return CreatedAtAction(nameof(GetEmprestimo), new { id = emprestimo.EmprestimoId }, emprestimo);
        }

        [HttpPut("devolver/{id}")]
        public IActionResult DevolverLivro(int id)
        {
            _emprestimoService.DevolverLivro(id);
            return NoContent();
        }

        [HttpGet]
        public ActionResult<IEnumerable<EmprestimoModel>> GetAllEmprestimos()
        {
            var emprestimos = _emprestimoService.GetAll();
            return Ok(emprestimos);
        }
    }
}
