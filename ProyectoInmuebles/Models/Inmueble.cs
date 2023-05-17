using System.ComponentModel.DataAnnotations;

namespace ProyectoInmuebles.Models
{
    public class Inmueble
    {
        [Display(Name = "ID Inmueble")]
        public int idInmueble { get; set; }

        [Display(Name = "Tipo Inmueble")]
        public int idTipoInmueble { get; set; }

        [Display(Name = "Inmueble")]
        public String descInmueble { get; set; } = string.Empty;

        [Display(Name = "Ubicación")]
        public string ubiInmueble { get; set; } = string.Empty;

        [Display(Name = "Costo")]
        public decimal costoInmueble { get; set; }

        [Display(Name = "Id Distrito")]
        public int idDistrito { get; set; }

        [Display(Name = "Imagen")]
        public string urlImagen { get; set; } = string.Empty;

        [Display(Name = "Distrito")]
        public string nombreDistrito { get; set; } = string.Empty;


        /*HOME*/
        [Display(Name = "Descripción tipo inmueble")]
        public string desc_tipo_Inmueble { get; set; } = string.Empty;
    }
}
