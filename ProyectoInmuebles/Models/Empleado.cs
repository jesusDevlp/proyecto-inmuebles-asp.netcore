using System.ComponentModel.DataAnnotations;

namespace ProyectoInmuebles.Models
{
    public class Empleado
    {
        [Display(Name = "ID Empleado")]
        public int idEmpleado { get; set; }

        [Display(Name = "Nombre ")]
        public string nombre { get; set; }

        [Display(Name = "Correo Electrónico")]
        public string correo { get; set; }

        [Display(Name = "Teléfono")]
        public string telefono { get; set; }

    }
}
