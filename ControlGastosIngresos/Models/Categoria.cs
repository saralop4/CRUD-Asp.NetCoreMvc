using System.ComponentModel.DataAnnotations;

namespace ControlGastosIngresos.Models
{
    public class Categoria
    {
        [Key] //esto son data anotation
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        [Display(Name ="Nombre Categoria")]
        public string NombreCategoria { get; set; }

        [Required]
        [MaxLength(2)]
        [Display(Name ="Tipo")]
        public string Tipo { get; set; } //IN ingreso y GA gasto 

        [Required]
        public  bool Estado { get; set; }
    }
}
