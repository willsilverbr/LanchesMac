using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Lanches")]
    public class Lanche
    {
        [Key]
        public int LancheId { get; set; }

        [Required(ErrorMessage ="O nome do Lanche deve ser informado")]
        [Display(Name = "Nome do Lanche")]
        [StringLength(80, MinimumLength = 10, ErrorMessage ="O {0} Deve ter no minimo {1} e no máximo {2}]")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="A descrição do lanche deve ser informada")]
        [Display(Name = "Descrição do Lanche")]
        [MinLength(20, ErrorMessage ="Descrição deve ter no minimo {1} caracteres" )]
        [MaxLength(200, ErrorMessage = "Descrição não pode exceder {1} caracteres}")]
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = "A descrição Detalhada deve ser informada")]
        [Display(Name = "Descrição Detalhada Do Lanche")]
        [MinLength(20, ErrorMessage = "Descrição detalhada deve ter no minimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição detalhada não pode exceder {1} caracteres}")]
        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "O preço do lanche deve ser informado")]
        [Display(Name = "Preço")]
        [Column(TypeName ="decimal(10,2)")]
        [Range(1,999.999, ErrorMessage ="O preço deve estar entre 1 e 999,99")]
        public decimal Preco { get; set; }

        [Display(Name = "Caminho da Imagem")]
        [StringLength(200, ErrorMessage = "O {0} Deve ter no máximo {1}]")]
        public string ImagemUrl { get; set; }

        [Display(Name = "Caminho da Imagem miniatura")]
        [StringLength(200, ErrorMessage = "O {0} Deve ter no máximo {1}]")]
        public string ImagemThmbnailUrl { get; set; }

        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido{ get; set; }

        [Display(Name = "Em Estoque")]
        public bool EmEstoque { get; set; }

        //[NotMapped] //Para não mapear no banco
        public int CategoriaID { get; set;  }
        public virtual Categoria Categoria { get; set; }
    }
}
