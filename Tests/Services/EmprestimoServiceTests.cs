using GestaoBibliotecaAPI.Interfaces;
using GestaoBibliotecaAPI.Model;
using GestaoBibliotecaAPI.Services;
using Moq;
using Xunit;

namespace GestaoBibliotecaAPI.Tests.Services
{
    public class EmprestimoServiceTests
    {
        private readonly Mock<IEmprestimoRepository> _mockEmprestimoRepo;
        private readonly Mock<ILivroRepository> _mockLivroRepo;
        private readonly EmprestimoService _emprestimoService;

        public EmprestimoServiceTests()
        {
            _mockEmprestimoRepo = new Mock<IEmprestimoRepository>();
            _mockLivroRepo = new Mock<ILivroRepository>();
            _emprestimoService = new EmprestimoService(_mockEmprestimoRepo.Object, _mockLivroRepo.Object);
        }

        [Fact]
        public void Deve_Criar_Emprestimo_Com_Sucesso()
        {
            // Arrange
            var livroId = 1;
            var livro = new LivroModel
            {
                LivroId = livroId,
                Titulo = "Teste",
                Autor = "Autor",
                AnoPublicacao = DateTime.Now,
                QuantidadeDisponivel = 1
            };

            _mockLivroRepo.Setup(repo => repo.Get(livroId))
                .Returns(livro);

            // Act
            var emprestimo = _emprestimoService.Create(livroId);

            // Assert
            Assert.NotNull(emprestimo);
            Assert.Equal(EmprestimoStatus.Ativo, emprestimo.Status);
            Assert.Equal(livroId, emprestimo.LivroId);
            _mockLivroRepo.Verify(repo => repo.UpdateQuantidade(livroId, -1), Times.Once);
        }

        [Fact]
        public void Nao_Deve_Criar_Emprestimo_Sem_Exemplares_Disponiveis()
        {
            // Arrange
            var livroId = 1;
            var livro = new LivroModel
            {
                LivroId = livroId,
                Titulo = "Teste",
                Autor = "Autor",
                AnoPublicacao = DateTime.Now,
                QuantidadeDisponivel = 0
            };

            _mockLivroRepo.Setup(repo => repo.Get(livroId))
                .Returns(livro);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _emprestimoService.Create(livroId));
        }

        [Fact]
        public void Deve_Devolver_Livro_Com_Sucesso()
        {
            // Arrange
            var emprestimoId = 1;
            var emprestimo = new EmprestimoModel
            {
                EmprestimoId = emprestimoId,
                LivroId = 1,
                Status = EmprestimoStatus.Ativo
            };

            _mockEmprestimoRepo.Setup(repo => repo.GetByLivroId(emprestimoId))
                .Returns(new[] { emprestimo });

            // Act
            _emprestimoService.DevolverLivro(emprestimoId);

            // Assert
            _mockEmprestimoRepo.Verify(repo => repo.UpdateStatus(emprestimoId, EmprestimoStatus.Devolvido), Times.Once);
            _mockLivroRepo.Verify(repo => repo.UpdateQuantidade(1, 1), Times.Once);
        }

        [Fact]
        public void Nao_Deve_Devolver_Livro_Ja_Devolvido()
        {
            // Arrange
            var emprestimoId = 1;
            var emprestimo = new EmprestimoModel
            {
                EmprestimoId = emprestimoId,
                LivroId = 1,
                Status = EmprestimoStatus.Devolvido
            };

            _mockEmprestimoRepo.Setup(repo => repo.GetByLivroId(emprestimoId))
                .Returns(new[] { emprestimo });

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _emprestimoService.DevolverLivro(emprestimoId));
        }
    }
}
