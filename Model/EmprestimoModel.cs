namespace GestaoBibliotecaAPI.Model
{
    public class EmprestimoModel
    {
        public int EmprestimoId { get; set; }
        public int LivroId { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public EmprestimoStatus Status { get; set; }

        public LivroModel? Livro { get; set; }

        public EmprestimoModel()
        {
            DataEmprestimo = DateTime.Now;
            Status = EmprestimoStatus.Ativo;
            DataDevolucao = DateTime.MinValue;
        }

        public void Devolver()
        {
            if (Status == EmprestimoStatus.Devolvido)
                throw new InvalidOperationException("Este empréstimo já foi devolvido");

            Status = EmprestimoStatus.Devolvido;
            DataDevolucao = DateTime.Now;
        }

        public void ValidarCriacao(LivroModel livro)
        {
            if (livro == null)
                throw new ArgumentException("Livro não encontrado");

            if (livro.QuantidadeDisponivel <= 0)
                throw new InvalidOperationException("Não há exemplares disponíveis para empréstimo");
        }
    }
    }

    public enum EmprestimoStatus
    {
        Ativo,
        Devolvido
    }
}
