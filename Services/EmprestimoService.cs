using GestaoBibliotecaAPI.Interfaces;
using GestaoBibliotecaAPI.Model;

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

        public EmprestimoModel Create(int livroId)
        {
            var livro = _livroRepository.Get(livroId);
            if (livro == null)
            {
                throw new ArgumentException("Livro não encontrado");
            }

            var emprestimosAtivos = _emprestimoRepository.GetByLivroId(livroId);
            if (emprestimosAtivos.Any(e => e.Status == EmprestimoStatus.Ativo))
            {
                throw new InvalidOperationException("Já existe um empréstimo ativo para este livro");
            }

            _emprestimoRepository.Create(livroId);
            _livroRepository.UpdateQuantidade(livroId, -1);

            var emprestimo = _emprestimoRepository.GetByLivroId(livroId).FirstOrDefault();
            if (emprestimo == null)
            {
                throw new InvalidOperationException("Erro ao criar empréstimo");
            }
            return emprestimo;
        }

        public void DevolverLivro(int emprestimoId)
        {
            var emprestimo = _emprestimoRepository.GetByLivroId(emprestimoId).FirstOrDefault();
            if (emprestimo == null)
            {
                throw new ArgumentException("Empréstimo não encontrado");
            }

            if (emprestimo.Status == EmprestimoStatus.Devolvido)
            {
                throw new InvalidOperationException("Este empréstimo já foi devolvido");
            }

            emprestimo.Status = EmprestimoStatus.Devolvido;
            emprestimo.DataDevolucao = DateTime.Now;

            _emprestimoRepository.UpdateStatus(emprestimoId, EmprestimoStatus.Devolvido);
            _livroRepository.UpdateQuantidade(emprestimo.LivroId, 1);
        }

        public IEnumerable<EmprestimoModel> GetAll()
        {
            return _emprestimoRepository.GetAll();
        }
    }
}
