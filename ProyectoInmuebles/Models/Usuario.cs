using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ProyectoInmuebles.Models
{
    public class Usuario
    {

        [Display(Name = "Codigo")]
        public int codigo { get; set; }

        [Display(Name = "Nombre")]
        public string? nombre { get; set; }

        [Display(Name = "Apellido")]
        public string? apellido { get; set; }

        [Display(Name = "Telefono")]
        public string? telefono { get; set; }

        [Required(ErrorMessage = "El Obligatorio El Email")]
        [Display(Name = "Email")]
        public string? email { get; set; }

        [Required(ErrorMessage = "El Obligatorio El Password")]
        [Display(Name = "Password")]
        public string? contra { get; set; }
    }
}
