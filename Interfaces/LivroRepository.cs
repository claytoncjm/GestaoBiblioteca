using GestaoBibliotecaAPI.Data;
using GestaoBibliotecaAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestaoBibliotecaAPI.Interfaces
{
    public class LivroRepository : ILivroRepository
    {
        private readonly BibliotecaDbContext _context;

        public LivroRepository(BibliotecaDbContext context)
        {
            _context = context;
        }

        public LivroModel Create(LivroModel livro)
        {
            if (string.IsNullOrEmpty(livro.Titulo) || string.IsNullOrEmpty(livro.Autor))
            {
                throw new ArgumentException("Título e autor são obrigatórios");
            }

            _context.Livros.Add(livro);
            _context.SaveChanges();
            return livro;
        }

        public LivroModel Get(int id)
        {
            return _context.Livros.Find(id);
        }

        public IEnumerable<LivroModel> GetAll()
        {
            return _context.Livros.ToList();
        }

        public void UpdateQuantidade(int livroId, int quantidade)
        {
            var livro = _context.Livros.Find(livroId);
            if (livro != null)
            {
                livro.QuantidadeDisponivel += quantidade;
                _context.SaveChanges();
            }
        }
    }
}
