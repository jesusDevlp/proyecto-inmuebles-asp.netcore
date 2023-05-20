using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoInmuebles.Models;

namespace ProyectoInmuebles.Controllers
{
    public class InmuebleController : Controller
    {

        private string cn;
        private readonly IWebHostEnvironment env;

        public InmuebleController(IConfiguration _config, IWebHostEnvironment env)
        {
            this.cn=_config.GetConnectionString("cn1");
            this.env = env;
        }

        private List<Inmueble> ListaInmuebles()
        {
            var listaInmuebles = new List<Inmueble>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cn, "sp_GetInmueble");

            while (dr.Read())
            {
                listaInmuebles.Add(new Inmueble()
                {
                    idInmueble = dr.GetInt32(0),
                    descInmueble = dr.GetString(1),
                    ubiInmueble = dr.GetString(2),
                    costoInmueble = dr.GetDecimal(3),
                    nombreDistrito = dr.GetString(4),
                    urlImagen=dr.GetString(5),
                    idDistrito = dr.GetInt32(6),
                    idTipoInmueble=dr.GetInt32(7)
                });
            }
            dr.Close();
            return listaInmuebles;
        }

        private List<Distrito> ListaDistritos()
        {
            var listaDistritos = new List<Distrito>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cn, "sp_GetListDistrito");


            while (dr.Read())
            {
                listaDistritos.Add(new Distrito()
                {
                    idDistrito=dr.GetInt32(0),

                    nombreDistrito=dr.GetString(1)

                });

            }

            dr.Close();
            return listaDistritos;
        }

        private List<TipoInmueble> ListaDeTipoInmueble()
        {

            var listaTipoInmuebles = new List<TipoInmueble>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cn, "sp_GetTipoInmueble");


            while (dr.Read())
            {
                listaTipoInmuebles.Add(new TipoInmueble()
                {
                    idTipoInmueble=dr.GetInt32(0),

                    descripInmueble=dr.GetString(1)

                });

            }

            dr.Close();
            return listaTipoInmuebles;
        }


        public IActionResult ListaDeInmuebles()
        {
            if (TempData["mensaje"] !=null)
            {
                ViewBag.mensaje=TempData["mensaje"];
            }
            return View(ListaInmuebles());
        }

        public IActionResult CreateInmuebles()
        {
            ViewBag.distritos=new SelectList(ListaDistritos(), "idDistrito", "nombreDistrito");
            ViewBag.tipoInmuebles=new SelectList(ListaDeTipoInmueble(), "idTipoInmueble", "descripInmueble");
            return View(new Inmueble());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateInmuebles(Inmueble im)
        {
            if (ModelState.IsValid)
            {
                Inmueble? existeInmueble = ListaInmuebles().FirstOrDefault(i => i.idInmueble == im.idInmueble);
                if (existeInmueble == null)
                {
                    if (im.formFile !=null)
                    {
                        string folder = "ImagenInmueble/";
                        im.urlImagen=DateTime.Now.ToString("ddmmyyyyhhmmss")+"-"+ im.formFile.FileName;
                        folder +=im.urlImagen;
                        string serverFolder = Path.Combine(env.WebRootPath, folder);

                        using (var fileStream = new FileStream(serverFolder, FileMode.Create))
                        {
                            im.formFile.CopyTo(fileStream);
                        }
                    }
                    else im.urlImagen="imagenDefault";
                    SqlHelper.ExecuteNonQuery(cn, "PA_GRABAR_INMUEBLE",
                    im.idInmueble, im.idTipoInmueble, im.descInmueble, im.ubiInmueble,
                    im.costoInmueble, im.idDistrito, im.urlImagen);
                    TempData["mensaje"]="Inmueble Registrado Correctamente";
                    return RedirectToAction("ListaDeInmuebles");
                }
                else ViewBag.mensaje="El Inmueble Con Codigo "+im.idInmueble+" ya esta Registrado,Ingrese otro codigo";
            }
            else ViewBag.mensaje="Fallo en la Validacion";

            ViewBag.distritos=new SelectList(ListaDistritos(), "idDistrito", "nombreDistrito");
            ViewBag.tipoInmuebles=new SelectList(ListaDeTipoInmueble(), "idTipoInmueble", "descripInmueble");
            return View(im);
        }

        public IActionResult EditInmuebles(int id)
        {

            Inmueble? im = ListaInmuebles().FirstOrDefault(i => i.idInmueble == id);
            if (im==null) return RedirectToAction("ListaDeInmuebles");
            else
            {
                ViewBag.distritos=new SelectList(ListaDistritos(), "idDistrito", "nombreDistrito", ""+im.idDistrito);
                ViewBag.tipoInmuebles=new SelectList(ListaDeTipoInmueble(), "idTipoInmueble", "descripInmueble", ""+im.idTipoInmueble);
                return View(im);
            }
        }

        [HttpPost]

        public IActionResult EditInmuebles(Inmueble im)
        {
            if (ModelState.IsValid)
            {
                if (im.formFile !=null)
                {
                    string folder = "ImagenInmueble/";
                    string folderEliminacion = Path.Combine(env.WebRootPath, folder, im.urlImagen);
                    if (System.IO.File.Exists(folderEliminacion)) System.IO.File.Delete(folderEliminacion);

                    im.urlImagen=DateTime.Now.ToString("ddmmyyyyhhmmss")+"-"+ im.formFile.FileName;
                    folder +=im.urlImagen;
                    string serverFolder = Path.Combine(env.WebRootPath, folder);

                    using (var fileStream = new FileStream(serverFolder, FileMode.Create))
                    {
                        im.formFile.CopyTo(fileStream);
                    }
                }
                SqlHelper.ExecuteNonQuery(cn, "PA_GRABAR_INMUEBLE",
                    im.idInmueble, im.idTipoInmueble, im.descInmueble, im.ubiInmueble,
                    im.costoInmueble, im.idDistrito, im.urlImagen);
                TempData["mensaje"]="Inmueble Actualizado Correctamente";
                return RedirectToAction("ListaDeInmuebles");
            }
            else ViewBag.mensaje="Fallo en la Validacion";

            ViewBag.distritos=new SelectList(ListaDistritos(), "idDistrito", "nombreDistrito", ""+im.idDistrito);
            ViewBag.tipoInmuebles=new SelectList(ListaDeTipoInmueble(), "idTipoInmueble", "descripInmueble", ""+im.idTipoInmueble);
            return View(im);
        }

       
        public IActionResult DeleteInmuebles(int id)
        {
            SqlHelper.ExecuteNonQuery(cn, "PA_ELIMINAR_INMUEBLE", id);
            TempData["mensaje"]="Inmueble Eliminado Correctamente";
            return RedirectToAction("ListaDeInmuebles");
        }
    }
}
