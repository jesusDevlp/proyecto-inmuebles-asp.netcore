using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;
using ProyectoInmuebles.Models;
using Newtonsoft.Json;

namespace ProyectoInmuebles.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string cad_cn = "";
        public HomeController(IConfiguration config, ILogger<HomeController> logger)
        {
            cad_cn = config.GetConnectionString("cn1");
            _logger = logger;
        }
        private bool VerificarSesion()
        {
            if (HttpContext.Session.GetString("usuario") == null)
            {
                return false;
            }
            return true;
        }
        private List<Inmueble> getInmuebles()
        {
            List<Inmueble> inmuebles = new List<Inmueble>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_cn, "sp_GetInmuebleHome");
            Inmueble? obj = null;
            while (dr.Read())
            {
                obj = new Inmueble()
                {
                    idInmueble = dr.GetInt32(0),
                    desc_tipo_Inmueble = dr.GetString(1),
                    descInmueble = dr.GetString(2),
                    ubiInmueble = dr.GetString(3),
                    nombreDistrito = dr.GetString(4),
                    urlImagen = dr.GetString(5),
                    costoInmueble = dr.GetDecimal(6)
                };
                inmuebles.Add(obj);
            }

            dr.Close();

            return inmuebles;
        }


        public IActionResult Index()
        {
            if (!this.VerificarSesion())
            {
                return RedirectToAction("IniciarSesion", "Usuario");
                ViewBag.session=false;
            }
            ViewBag.session=true;
            ViewBag.email=HttpContext.Session.GetString("email") as string;            
            return View(getInmuebles());
        }

        public IActionResult Privacy()
        {
            if (!this.VerificarSesion())
            {
                return RedirectToAction("IniciarSesion", "Usuario");
                ViewBag.session=false;
            }
            ViewBag.session=true;
            ViewBag.email=HttpContext.Session.GetString("email") as string;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}