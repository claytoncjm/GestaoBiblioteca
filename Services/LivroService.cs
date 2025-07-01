using GestaoBibliotecaAPI.Interfaces;
using GestaoBibliotecaAPI.Model;
using System.Threading.Tasks;

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

        public async Task<LivroModel> CriarLivroAsync(LivroModel livro)
        {
            return await _livroRepository.CriarLivroAsync(livro);
        }

        public async Task<LivroModel> ObterLivroPorIdAsync(int id)
        {
            return await _livroRepository.ObterLivroPorIdAsync(id);
        }

        public async Task<IEnumerable<LivroModel>> ListarLivrosAsync()
        {
            return await _livroRepository.ListarLivrosAsync();
        }

        public async Task AtualizarQuantidadeDisponivelAsync(int livroId, int quantidade)
        {
            await _livroRepository.AtualizarQuantidadeDisponivelAsync(livroId, quantidade);
        }
    }
}
