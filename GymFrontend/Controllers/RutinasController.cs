using BL;
using DTO.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymFrontend.Controllers
{
    public class RutinasController:Controller
    {
        [HttpGet]
        [Route("gestionRutinas")]
        public IActionResult Index()
        {
            string pageToRender = "GestionRutinas";
           
            UsersManager manager = new UsersManager();
            HttpResponseListadoQueryUsers respuestaClientes = manager.obtenerUsuariosPorRol("cliente", "activo").Result;
            HttpResponseListadoQueryUsers respuestaEntrenadores = manager.obtenerUsuariosPorRol("empleado", "activo").Result;
            ViewBag.clientes = respuestaClientes.data;
            ViewBag.entrenadores = respuestaEntrenadores.data;
            return View("GestionRutinas", new Rutina());
            
        }
        [HttpGet]
        [Route("asignarEjercicios")]
        public IActionResult asignarEjercicios([FromQuery] int UsuarioId)
        {
            UsersManager manager = new UsersManager();
            RutinasManager rutinasManager = new RutinasManager();
            EjerciciosManager ejericiosManager = new EjerciciosManager();
            HttpResponseListadoQueryUsers respuestaClientes = manager.obtenerUsuariosPorRol("cliente", "activo").Result; ;
            ViewBag.clientes = respuestaClientes.data;
            if (UsuarioId > 0) {
                HttpResponseListadoRutinas respuestaRutinas = rutinasManager.obtenerRutinasDeUsuario(UsuarioId).Result;
                HttpResponseListadoEjercicios respuestaEjercicios = ejericiosManager.obtenerEjercicios().Result;
                ViewBag.rutinas = respuestaRutinas.data;
                ViewBag.ejercicios = respuestaEjercicios.data;
            }
            return View("AsignacionEjercicios");
        }
           
    [HttpPost]
        [Route("gestionRutinas")]
        public IActionResult gestionarRutinas(Rutina rutina)
        {
            rutina.FechaCreacion = DateTime.Now;
            UsersManager usersManager = new UsersManager();
            HttpResponseListadoQueryUsers respuestaClientes = usersManager.obtenerUsuariosPorRol("cliente", "activo").Result;
            HttpResponseListadoQueryUsers respuestaEntrenadores = usersManager.obtenerUsuariosPorRol("empleado", "activo").Result;
            ViewBag.clientes = respuestaClientes.data;
            ViewBag.entrenadores = respuestaEntrenadores.data;
            if (!ModelState.IsValid)
            {
                return View("GestionRutinas", rutina);
            }
            RutinasManager rutinasManager = new RutinasManager();
            
            HttpResponseRutina respuesta = rutinasManager.registrarRutina(rutina).Result;
            if (respuesta.ok == false)
            {
                ViewBag.message = respuesta.message;
                return View("GestionRutinas");
            }
            ViewBag.message = respuesta.message;
            return View("GestionRutinas");
        }


        [HttpPost]
        [Route("asignarEjercicios")]
        public IActionResult asignarRutina(AsignacionDeEjercicio asignacion)
        {
            UsersManager usersManager = new UsersManager();
            HttpResponseListadoQueryUsers respuestaClientes = usersManager.obtenerUsuariosPorRol("cliente", "activo").Result;
            ViewBag.clientes = respuestaClientes.data;
            if (!ModelState.IsValid)
            {
                return View("AsignacionEjercicios", asignacion);
            }
            RutinasManager manager = new RutinasManager();
            HttpResponseAsignacion respuesta = manager.asignarEjercicioARutina(asignacion).Result;
            if (respuesta.ok == false)
            {
                ViewBag.message = respuesta.message;
                return View("AsignacionEjercicios",asignacion);
            }
            ViewBag.message = respuesta.message;
            return View("AsignacionEjercicios",asignacion);
        }
    }
}
