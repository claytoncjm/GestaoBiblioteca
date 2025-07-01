using GestaoBibliotecaAPI.Interfaces;
using GestaoBibliotecaAPI.Model;

namespace GestaoBibliotecaAPI.Services
{
    public class LivroService
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IEmprestimoRepository _emprestimoRepository;

        public LivroService(ILivroRepository livroRepository, IEmprestimoRepository emprestimoRepository)
        {
            _livroRepository = livroRepository;
            _emprestimoRepository = emprestimoRepository;
        }

        public LivroModel Create(LivroModel livro)
        {
            return _livroRepository.Create(livro);
        }

        public LivroModel Get(int id)
        {
            return _livroRepository.Get(id);
        }

        public IEnumerable<LivroModel> GetAll()
        {
            return _livroRepository.GetAll();
        }

        public void UpdateQuantidade(int livroId, int quantidade)
        {
            _livroRepository.UpdateQuantidade(livroId, quantidade);
        }
    }
}
