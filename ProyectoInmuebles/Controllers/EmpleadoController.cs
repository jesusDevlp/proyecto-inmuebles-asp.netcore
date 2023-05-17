using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoInmuebles.Models;

namespace ProyectoInmuebles.Controllers
{
    public class EmpleadoController : Controller
    {

        private string cad_cn = "";


        public EmpleadoController(IConfiguration config)
        {
            cad_cn = config.GetConnectionString("cn1");
        }

     
        private List<Empleado> getEmpleados()
        {
            List<Empleado> empleados = new List<Empleado>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_cn, "sp_GetEmpleado");
            Empleado obj = null;
            while (dr.Read())
            {
                obj = new Empleado()
                {
                    idEmpleado = dr.GetInt32(0),
                    nombre = dr.GetString(1),   
                    correo = dr.GetString(2),
                    telefono = dr.GetString(3)

                };
                empleados.Add(obj);
            }

            dr.Close();

            return empleados;
        }



        // GET: EmpleadoController
        public ActionResult IndexEmpleados()
        {
            return View(getEmpleados());
        }

        // GET: EmpleadoController/Details/5
        public ActionResult DetailsEmpleado(int id)
        {
            Empleado? buscado = getEmpleados().Find(empleado => empleado.idEmpleado.Equals(id));
            return View(buscado);
        }

        // GET: EmpleadoController/Create
        public ActionResult CreateEmpleado()
        {
            Empleado empleado = new Empleado();
            return View(empleado);
        }

        // POST: EmpleadoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEmpleado(Empleado empleado)
        {
            try
            {
                if(ModelState.IsValid == true)
                 {
                SqlHelper.ExecuteNonQuery(cad_cn, "PA_GRABAR_EMPLEADO",
                    empleado.idEmpleado, empleado.nombre, empleado.correo,
                    empleado.telefono);

                ViewBag.MENSAJE = "Empleado Registrado correctamente";
                 }

            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }
            return View(empleado);
        }

        // GET: EmpleadoController/Edit/5
        public ActionResult EditEmpleado(int id)
        {
            Empleado? buscado = getEmpleados().Find(empleado => empleado.idEmpleado.Equals(id));
            return View(buscado);
        }

        // POST: EmpleadoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmpleado(int id, Empleado empleado)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_cn, "PA_GRABAR_EMPLEADO",
                empleado.idEmpleado, empleado.nombre, empleado.correo,
                empleado.telefono);

                //
                ViewBag.MENSAJE = $"El Empleado {empleado.idEmpleado}"+ " editado correctamente";
              
            }
            catch   (Exception ex)
            {
                ViewBag.MENSAJE = "Error: " + ex.Message;
            }
            return View(empleado);
        }

        // GET: EmpleadoController/Delete/5
        public ActionResult DeleteEmpleado(int id)
        {
            Empleado? buscado = getEmpleados().Find(empleado => empleado.idEmpleado.Equals(id));
            return View(buscado);
        }

        // POST: EmpleadoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEmpleado(int id, IFormCollection collection)
        {

            Empleado? buscado = getEmpleados().Find(empleado => empleado.idEmpleado.Equals(id));

            try
            {
                SqlHelper.ExecuteNonQuery(cad_cn, "PA_ELIMINAR_EMPLEADO", id);

                ViewBag.MENSAJE = $"El Empleado con el codigo {id}" + " fue eliminado de forma lógica";

                return RedirectToAction("IndexEmpleados");
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }
            return View(buscado);
        }
    }
}
