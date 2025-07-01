using GestaoBibliotecaAPI.Model;
using Xunit;

namespace GestaoBibliotecaAPI.Tests.Models
{
    public class EmprestimoModelTests
    {
        [Fact]
        public void Deve_Criar_Emprestimo_Com_Sucesso()
        {
            // Arrange
            var livro = new LivroModel
            {
                LivroId = 1,
                Titulo = "Teste",
                Autor = "Autor",
                AnoPublicacao = DateTime.Now,
                QuantidadeDisponivel = 1
            };

            var emprestimo = new EmprestimoModel();

            // Act
            emprestimo.ValidarCriacao(livro);

            // Assert
            Assert.Equal(EmprestimoStatus.Ativo, emprestimo.Status);
            Assert.NotNull(emprestimo.DataEmprestimo);
        }

        [Fact]
        public void Nao_Deve_Criar_Emprestimo_Sem_Exemplares_Disponiveis()
        {
            // Arrange
            var livro = new LivroModel
            {
                LivroId = 1,
                Titulo = "Teste",
                Autor = "Autor",
                AnoPublicacao = DateTime.Now,
                QuantidadeDisponivel = 0
            };

            var emprestimo = new EmprestimoModel();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => 
                emprestimo.ValidarCriacao(livro));

            Assert.Equal("Não há exemplares disponíveis para empréstimo", exception.Message);
        }


    }
}
