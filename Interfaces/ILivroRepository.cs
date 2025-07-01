using GestaoBibliotecaAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoBibliotecaAPI.Interfaces
{
    public interface ILivroRepository
    {
        LivroModel Create(LivroModel livro);
        LivroModel Get(int id);
        IEnumerable<LivroModel> GetAll();
        void UpdateQuantidade(int livroId, int quantidade);
    }
}
