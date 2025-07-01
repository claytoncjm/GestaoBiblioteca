using GestaoBibliotecaAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace GestaoBibliotecaAPI.Data
{
    public class ContextDB :DbContext
    {
        public ContextDB(DbContextOptions <ContextDB> opts) : base (opts)
        {
                
        }

        public DbSet<LivroModel> Livros { get; set; }
        public DbSet<EmprestimoModel> Emprestimos { get; set; }
    }
}
