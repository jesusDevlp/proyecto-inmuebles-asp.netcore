using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using ProyectoInmuebles.Models;

namespace ProyectoInmuebles.Controllers
{
    public class UsuarioController : Controller
    {
        private string cad_cn = "";

        public UsuarioController(IConfiguration config)
        {
            cad_cn = config.GetConnectionString("cn1");
        }

        private bool VerificarSesion()
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return false;
            }
            return true;
        }

        public IActionResult IniciarSesion()
        {             
            ViewBag.session=false;            
            return View(new Usuario());
        }

        [HttpPost]
        public IActionResult IniciarSesion(Usuario usuario)
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(this.cad_cn,"INICIAR_SESION",usuario.email,usuario.contra);
            if (dr.Read())
            {
                usuario.codigo=dr.GetInt32(0);
                usuario.nombre=dr.GetString(1);
                usuario.apellido=dr.GetString(2);
                usuario.telefono=dr.GetString(3);
                usuario.email=dr.GetString(4);
                usuario.contra=dr.GetString(5);
               
                HttpContext.Session.SetString("usuario", JsonConvert.SerializeObject(usuario));
                HttpContext.Session.SetString("email",usuario.email);

                return RedirectToAction("Index", "Home");
            }
            ViewBag.session=false;
            ViewBag.error="Credenciales Incorrectas";
            return View(usuario);
        }

        public IActionResult CerrarSesion()
        {
            
            HttpContext.Session.Clear();
            return RedirectToAction("IniciarSesion");
        }

    }
}
