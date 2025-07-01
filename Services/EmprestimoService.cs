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

        public async Task<EmprestimoModel> CreateAsync(int livroId)
        {
            var livro = await _livroRepository.ObterLivroPorIdAsync(livroId);
            if (livro == null)
            {
                throw new ArgumentException("Livro não encontrado");
            }

            var emprestimosAtivos = await _emprestimoRepository.ObterEmprestimosPorLivroIdAsync(livroId);
            if (emprestimosAtivos.Any(e => e.Status == "Ativo"))
            {
                throw new InvalidOperationException("Já existe um empréstimo ativo para este livro");
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
            var emprestimos = await _emprestimoRepository.ObterEmprestimosPorLivroIdAsync(emprestimoId);
            var emprestimo = emprestimos.FirstOrDefault(e => e.EmprestimoId == emprestimoId);

            if (emprestimo == null)
            {
                throw new ArgumentException("Empréstimo não encontrado");
            }

            if (emprestimo.Status == "Devolvido")
            {
                throw new InvalidOperationException("Este empréstimo já foi devolvido");
            }

            emprestimo.Status = "Devolvido";
            emprestimo.DtaDevolucao = DateTime.Now;

            await _emprestimoRepository.AtualizarStatusEmprestimoAsync(emprestimoId, "Devolvido");
            await _livroRepository.AtualizarQuantidadeDisponivelAsync(emprestimo.LivroId, 1);
        }

        public async Task<IEnumerable<EmprestimoModel>> GetAllAsync()
        {
            return await _emprestimoRepository.ListarEmprestimosAsync();
        }
    }
}
