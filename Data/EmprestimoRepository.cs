using GestaoBibliotecaAPI.Interfaces;
using GestaoBibliotecaAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GestaoBibliotecaAPI.Data
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
            var emprestimo = new EmprestimoModel
            {
                LivroId = livroId,
                DataEmprestimo = DateTime.Now,
                Status = EmprestimoStatus.Ativo
            };

            _context.Emprestimos.Add(emprestimo);
            _context.SaveChanges();
            return emprestimo;
        }

        public void UpdateStatus(int emprestimoId, EmprestimoStatus novoStatus)
        {
            var emprestimo = _context.Emprestimos.Find(emprestimoId);
            if (emprestimo != null)
            {
                emprestimo.Status = novoStatus;
                if (novoStatus == EmprestimoStatus.Devolvido)
                {
                    emprestimo.DataDevolucao = DateTime.Now;
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
            return _context.Emprestimos.Where(e => e.LivroId == livroId).ToList();
        }
    }
}
