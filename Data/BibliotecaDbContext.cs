using Microsoft.EntityFrameworkCore;
using GestaoBibliotecaAPI.Model;

namespace GestaoBibliotecaAPI.Data
{
    public class BibliotecaDbContext : DbContext
    {
        public DbSet<LivroModel> Livros { get; set; }
        public DbSet<EmprestimoModel> Emprestimos { get; set; }

        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmprestimoModel>()
                .HasOne(e => e.Livro)
                .WithMany(l => l.Emprestimos)
                .HasForeignKey(e => e.LivroId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LivroModel>()
                .Property(l => l.Titulo)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<LivroModel>()
                .Property(l => l.Autor)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<EmprestimoModel>()
                .Property(e => e.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasCheckConstraint("CK_Emprestimo_Status", "[Status] IN ('Ativo', 'Devolvido')");
        }
    }
}
