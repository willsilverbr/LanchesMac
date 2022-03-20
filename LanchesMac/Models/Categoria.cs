using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [StringLength(100, ErrorMessage ="O tamanho Maximo é 100 Caracteres")]
        [Required(ErrorMessage ="Informe o nome da categoria")]
        [Display(Name = "Nome")]
        public string CategoriaNome { get; set; }

        [StringLength(200, ErrorMessage = "O tamanho Maximo é 200 Caracteres")]
        [Required(ErrorMessage = "Informe a Descrição da categoria")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public List<Lanche> Lanches { get; set; }   
    }
}
