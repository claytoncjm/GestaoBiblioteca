using GestaoBibliotecaAPI.Interfaces;
using GestaoBibliotecaAPI.Model;
using System.Threading.Tasks;

namespace GestaoBibliotecaAPI.Services
{
    public class EmprestimoService
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly ILivroRepository _livroRepository;

        public EmprestimoService(IEmprestimoRepository emprestimoRepository, ILivroRepository livroRepository)
        {
            _emprestimoRepository = emprestimoRepository;
            _livroRepository = livroRepository;
        }

        public async Task<EmprestimoModel> CriarEmprestimoAsync(int livroId)
        {
            var livro = await _livroRepository.ObterLivroPorIdAsync(livroId);
            if (livro == null)
            {
                throw new ArgumentException("Livro não encontrado");
            }

            if (livro.QuantidadeDisponivel <= 0)
            {
                throw new InvalidOperationException("Não há exemplares disponíveis para empréstimo");
            }

            var emprestimo = new EmprestimoModel
            {
                LivroId = livroId,
                DtaEmprestimo = DateTime.Now,
                Status = "Ativo"
            };

            await _emprestimoRepository.CriarEmprestimoAsync(emprestimo);
            await _livroRepository.AtualizarQuantidadeDisponivelAsync(livroId, -1);

            return emprestimo;
        }

        public async Task DevolverLivroAsync(int emprestimoId)
        {
            var emprestimo = await _emprestimoRepository.ObterEmprestimosPorLivroIdAsync(emprestimoId).FirstOrDefaultAsync();
            if (emprestimo == null)
            {
                throw new ArgumentException("Empréstimo não encontrado");
            }

            if (emprestimo.Status == "Devolvido")
            {
                throw new InvalidOperationException("Este livro já foi devolvido");
            }

            await _emprestimoRepository.AtualizarStatusEmprestimoAsync(emprestimoId, "Devolvido");
            await _livroRepository.AtualizarQuantidadeDisponivelAsync(emprestimo.LivroId, 1);
        }

        public async Task<IEnumerable<EmprestimoModel>> ListarEmprestimosAsync()
        {
            return await _emprestimoRepository.ListarEmprestimosAsync();
        }
    }
}
