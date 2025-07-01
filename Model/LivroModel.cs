using System;
using System.ComponentModel.DataAnnotations;

namespace GestaoBibliotecaAPI.Model
{
    public class LivroModel
    {
        [Key]
        public int LivroId { get; set; }
        public string? Titulo { get; set; }
        public string? Autor { get; set; }
        public DateTime AnoPublicacao { get; set; }
        public int QuantidadeDisponivel { get; set; }

        public ICollection<EmprestimoModel>? Emprestimos { get; set; }

        public LivroModel()
        {
            Emprestimos = new List<EmprestimoModel>();
        }
    }
}
