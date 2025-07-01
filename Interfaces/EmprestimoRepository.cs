using GestaoBibliotecaAPI.Data;
using GestaoBibliotecaAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestaoBibliotecaAPI.Interfaces
{
    public class EmprestimoRepository : IEmprestimoRepository
    {
        private readonly BibliotecaDbContext _context;

        public EmprestimoRepository(BibliotecaDbContext context)
        {
            _context = context;
        }

        public EmprestimoModel Create(int livroId)
        {
            var livro = _context.Livros.Find(livroId);
            if (livro == null)
                throw new ArgumentException("Livro não encontrado");

            if (livro.QuantidadeDisponivel <= 0)
                throw new InvalidOperationException("Não há exemplares disponíveis para empréstimo");

            var emprestimo = new EmprestimoModel
            {
                LivroId = livroId,
                Livro = livro
            };

            emprestimo.ValidarCriacao(livro);
            _context.Emprestimos.Add(emprestimo);
            _context.SaveChanges();
            return emprestimo;
        }

        public void UpdateStatus(int emprestimoId, string novoStatus)
        {
            var emprestimo = _context.Emprestimos.Find(emprestimoId);
            if (emprestimo != null)
            {
                if (novoStatus == "Devolvido")
                {
                    emprestimo.Devolver();
                }
                _context.SaveChanges();
            }
        }

        public IEnumerable<EmprestimoModel> GetAll()
        {
            return _context.Emprestimos.ToList();
        }

        public IEnumerable<EmprestimoModel> GetByLivroId(int livroId)
        {
            return _context.Emprestimos
                .Where(e => e.LivroId == livroId)
                .ToList();
        }
    }
}
