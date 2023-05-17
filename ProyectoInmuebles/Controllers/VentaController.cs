using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoInmuebles.Models;

namespace ProyectoInmuebles.Controllers
{


    public class VentaController : Controller
    {

        private string cad_cn = "";

       
        public VentaController(IConfiguration config)
        {
            cad_cn = config.GetConnectionString("cn1");
        }

        private List<TipoInmueble> getTipoInmuebles()
        {
            List<TipoInmueble> tipoInmuebles = new List<TipoInmueble>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_cn, "sp_GetTipoInmueble");
            TipoInmueble obj = null;
            while (dr.Read())
            {
                obj = new TipoInmueble()
                {
                    idTipoInmueble = dr.GetInt32(0),
                    descripInmueble = dr.GetString(1)
                    
                };
                tipoInmuebles.Add(obj);
            }  

            dr.Close();

            return tipoInmuebles;
        }

        private List<Inmueble> getInmuebles()
        {
            List<Inmueble> inmuebles = new List<Inmueble>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_cn, "sp_GetInmueble");
            Inmueble obj = null;
            while (dr.Read())
            {
                obj = new Inmueble()
                {
                   idInmueble = dr.GetInt32(0),
                   descInmueble = dr.GetString(1)

                };
                inmuebles.Add(obj);
            }

            dr.Close();

            return inmuebles;
        }




        private List<FormaPago> getFormaPago()
        {
            List<FormaPago> formaPago = new List<FormaPago>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_cn, "sp_GetFormaPago");
            FormaPago obj = null;
            while (dr.Read())
            {
                obj = new FormaPago()
                {
                    idFormaPago = dr.GetInt32(0),
                    descripPago = dr.GetString(1)

                };
                formaPago.Add(obj);
            }

            dr.Close();

            return formaPago;
        }

        private List<Condicion> getCondicion()
        {
            List<Condicion> condicion = new List<Condicion>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_cn, "sp_GetCondicion");
            Condicion obj = null;
            while (dr.Read())
            {
                obj = new Condicion()
                {
                    idCondicion = dr.GetInt32(0),
                    descripCondicion = dr.GetString(1)

                };
                condicion.Add(obj);
            }

            dr.Close();

            return condicion;
        }

        private List<Empleado> getEmpleadoVentas()
        {
            List<Empleado> empleado = new List<Empleado>();
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
                empleado.Add(obj);
            }

            dr.Close();

            return empleado;
        }

        private List<Venta> getVentas()
        {
            List<Venta> ventas = new List<Venta>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_cn, "sp_GetVenta");
            Venta obj = null;
            while (dr.Read())
            {
                obj = new Venta()
                {
                    idVenta = dr.GetInt32(0),
                    idInmueble = dr.GetInt32(1),
                    descripInmueble = dr.GetString (2),
                    idEmpleado = dr.GetInt32 (3),
                    nombreEmpleado = dr.GetString (4),
                    nombreCliente = dr.GetString (5),
                    nroDocumento = dr.GetString(6),
                    idCondicion = dr.GetInt32 (7),
                    descrpCondicion = dr.GetString (8),
                    idFormaPago = dr.GetInt32 (9),
                    descripFormaPago = dr.GetString (10),
                    totalVenta = dr.GetDecimal(11),
                    idTipoInmueble = dr.GetInt32(12), 
                    descripTipoInmueble = dr.GetString(13),
                    totalGeneral = dr.GetDecimal(14),
                    fechaVenta = dr.GetDateTime(15)

                };
                ventas.Add(obj);
            }

            dr.Close();

            return ventas;
        }


        // GET: VentaController
        public ActionResult IndexVentas()
        {
            return View(getVentas());
        }

        // GET: VentaController/Details/5
        public ActionResult DetailsVentas(int id)
        {
            Venta? buscado = getVentas().Find(ventas => ventas.idVenta.Equals(id)); 
            return View(buscado);
        }

        // GET: VentaController/Create
        public ActionResult CreateVentas()
        {
            Venta venta = new Venta();
            ViewBag.tipos = new SelectList(getTipoInmuebles(), "idTipoInmueble", "descripInmueble");
            ViewBag.inmuebles = new SelectList(getInmuebles(), "idInmueble", "descInmueble");
            ViewBag.pagos = new SelectList(getFormaPago(), "idFormaPago", "descripPago");
            ViewBag.condiciones = new SelectList(getCondicion(), "idCondicion", "descripCondicion");
            ViewBag.empleado = new SelectList(getEmpleadoVentas(), "idEmpleado", "nombre");

            return View(venta);
        }

        // POST: VentaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateVentas(Venta venta)
        {
            try
            {
                //if(ModelState.IsValid == true)
             //   {
                    SqlHelper.ExecuteNonQuery(cad_cn, "PA_GRABAR_VENTA",
                        venta.idVenta, venta.idInmueble, venta.idEmpleado,
                        venta.nombreCliente, venta.nroDocumento,
                        venta.idCondicion, venta.idFormaPago, venta.totalVenta,
                        venta.idTipoInmueble, venta.totalGeneral, venta.fechaVenta);

                    ViewBag.MENSAJE = "Venta Registrada correctamente";
              //  }
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }

            ViewBag.tipos = new SelectList(getTipoInmuebles(), "idTipoInmueble", "descripInmueble");
            ViewBag.inmuebles = new SelectList(getInmuebles(), "idInmueble", "descInmueble");
            ViewBag.pagos = new SelectList(getFormaPago(), "idFormaPago", "descripPago");
            ViewBag.condiciones = new SelectList(getCondicion(), "idCondicion", "descripCondicion");
            ViewBag.empleado = new SelectList(getEmpleadoVentas(), "idEmpleado", "nombre");

            return View(venta);
        }

        // GET: VentaController/Edit/5
        public ActionResult EditVentas(int id)
        {
            Venta? buscado = getVentas().Find(ventas => ventas.idVenta.Equals(id));

            ViewBag.tipos = new SelectList(getTipoInmuebles(), "idTipoInmueble", "descripInmueble");
            ViewBag.inmuebles = new SelectList(getInmuebles(), "idInmueble", "descInmueble");
            ViewBag.pagos = new SelectList(getFormaPago(), "idFormaPago", "descripPago");
            ViewBag.condiciones = new SelectList(getCondicion(), "idCondicion", "descripCondicion");
            ViewBag.empleado = new SelectList(getEmpleadoVentas(), "idEmpleado", "nombre");



            return View(buscado);
        }

        // POST: VentaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVentas(int id, Venta venta)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(cad_cn, "PA_GRABAR_VENTA",
                venta.idVenta, venta.idInmueble, venta.idEmpleado,
                        venta.nombreCliente, venta.nroDocumento,
                        venta.idCondicion, venta.idFormaPago, venta.totalVenta,
                        venta.idTipoInmueble, venta.totalGeneral, venta.fechaVenta);

                //

                ViewBag.MENSAJE = $"La venta {venta.idVenta}" + " fue actualizada correctamente";

            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = "Error: " + ex.Message;
            }
            ViewBag.tipos = new SelectList(getTipoInmuebles(), "idTipoInmueble", "descripInmueble");
            ViewBag.inmuebles = new SelectList(getInmuebles(), "idInmueble", "descInmueble");
            ViewBag.pagos = new SelectList(getFormaPago(), "idFormaPago", "descripPago");
            ViewBag.condiciones = new SelectList(getCondicion(), "idCondicion", "descripCondicion");
            ViewBag.empleado = new SelectList(getEmpleadoVentas(), "idEmpleado", "nombre");

            return View(venta);
        }

        // GET: VentaController/Delete/5
        public ActionResult DeleteVentas(int id)
        {
            Venta? buscado = getVentas().Find(ventas => ventas.idVenta.Equals(id));
            return View(buscado);
           
        }

        // POST: VentaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteVentas(int id, IFormCollection collection)
        {
            Venta? buscado = getVentas().Find(ventas => ventas.idVenta.Equals(id));


            try
            {
                SqlHelper.ExecuteNonQuery(cad_cn, "PA_ELIMINAR_VENTA", id);

                TempData["MENSAJE"] = $"La venta con el Codigo {id} " +
                                     "Fue Eliminada de forma lógica";

                return RedirectToAction("IndexVentas");
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
                
            }
            return View(buscado);
        }
    }
}
