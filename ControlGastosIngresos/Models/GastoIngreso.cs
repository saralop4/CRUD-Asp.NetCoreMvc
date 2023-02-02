using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ControlGastosIngresos.Models
{
    public class GastoIngreso
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }

        [Required]
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        [Required]
        [Range(1, 100000)]
        [DisplayFormat(DataFormatString = "{0:C}")] //para que se imprima en formato de moneda
        [Display(Name = "Valor")]
        public double Valor { get; set; }
    }
}
