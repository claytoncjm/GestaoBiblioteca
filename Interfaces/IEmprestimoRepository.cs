using GestaoBibliotecaAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoBibliotecaAPI.Interfaces
{
    public interface IEmprestimoRepository
    {
        EmprestimoModel Create(int livroId);
        void UpdateStatus(int emprestimoId, EmprestimoStatus novoStatus);
        IEnumerable<EmprestimoModel> GetAll();
        IEnumerable<EmprestimoModel> GetByLivroId(int livroId);
    }
}
