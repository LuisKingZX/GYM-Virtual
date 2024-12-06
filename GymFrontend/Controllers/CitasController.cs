﻿using BL;
using DTO.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GymFrontend.Controllers
{
    public class CitasController : Controller
    {
        [HttpGet]
        [Route("registrarCita")]
        public IActionResult Index()
        {
            UsersManager manager = new UsersManager();
            HttpResponseListadoQueryUsers respuestaClientes = manager.obtenerUsuariosPorRol("cliente", "activo").Result;
            ViewBag.clientes = respuestaClientes.data;
            return View("RegistrarCita", new Cita());

        }

        [HttpPost]
        [Route("registrarCita")]
        public IActionResult registrarCita(Cita cita)
        {
            UsersManager managerUsers = new UsersManager();
            HttpResponseListadoQueryUsers respuestaClientes = managerUsers.obtenerUsuariosPorRol("cliente", "activo").Result;
            ViewBag.clientes = respuestaClientes.data;
            cita.estado = "Agendada";
            if (!ModelState.IsValid)
            {
                return View("RegistrarCita", cita);
            }
            CitasManager manager = new CitasManager();
            
            HttpResponseCita respuesta = manager.registrarCita(cita).Result;
                        if (respuesta.ok == false)
            {
                ViewBag.message = respuesta.message;
                return View("RegistrarCita",cita);
            }
            ViewBag.message = respuesta.message;
            return View("RegistrarCita",cita);
        }


        [HttpGet]
        [Route("modificarCita")]
        public IActionResult modificarCita(CitaEdit cita)
        {
            UsersManager managerUsers = new UsersManager();
            HttpResponseListadoQueryUsers respuestaClientes = managerUsers.obtenerUsuariosPorRol("cliente", "activo").Result;
            ViewBag.clientes = respuestaClientes.data;
            if (respuestaClientes.ok == false)
            {
                ViewBag.message = respuestaClientes.message;
                return View("ModificarCita", cita);
            }
            ViewBag.message = "";
            return View("ModificarCita", new CitaEdit());
        }

        [HttpPost]
        [Route("modificarCita")]
        public IActionResult modificarCitaPost(CitaEdit cita)
        {
            UsersManager managerUsers = new UsersManager();
            HttpResponseListadoQueryUsers respuestaClientes = managerUsers.obtenerUsuariosPorRol("cliente", "activo").Result;
            ViewBag.clientes = respuestaClientes.data;
            if (!ModelState.IsValid)
            {
                return View("ModificarCita", cita);
            }

            
            CitasManager citasManager = new CitasManager();
            var respuestaCita = citasManager.modificarCita(cita).Result;
            
            if (respuestaCita.ok == false)
            {
                ViewBag.message = respuestaClientes.message;
                return View("ModificarCita", cita);
            }
            ViewBag.message = respuestaCita.message;
            return View("ModificarCita", cita);
        }

        [HttpGet]
        [Route("duracionCitasMedicionPage")]
        public IActionResult renderVistaDuracion(DuracionCitasMedicion duracion)
        {
            CitasManager manager = new CitasManager();
            HttpResponseDuracionCitasMedicion response = manager.obtenerDuracionCitasMedicion().Result;
            DuracionCitasMedicion duracionExistente = response.data;
            ViewBag.duracion = duracionExistente;
            if (duracionExistente == null) {
                ViewBag.message = "No tienes configuracion de duracion de citas de medicion aun, registra una ";
                return View("DuracionCitasMedicion", new DuracionCitasMedicion());
            }
           
            ViewBag.message = "";
            return View("DuracionCitasMedicion", new DuracionCitasMedicion() {Id= duracionExistente.Id,duracion=duracionExistente.duracion,frecuenciaNotificaciones = duracionExistente.frecuenciaNotificaciones });
        }


        [HttpPost]
        [Route("duracionCitasMedicionPage")]
        public IActionResult gestionarDuracionCitasMedicion(DuracionCitasMedicion duracionParam)
        {
            CitasManager manager = new CitasManager();
            HttpResponseDuracionCitasMedicion responsePost = manager.registrarDuracionCitasMedicion(duracionParam).Result;
            HttpResponseDuracionCitasMedicion response = manager.obtenerDuracionCitasMedicion().Result;
            DuracionCitasMedicion duracionExistente = response.data;
                ViewBag.duracion = duracionExistente;
            if (duracionExistente == null)
            {
                ViewBag.message = "No tienes configuracion de duracion de citas de medicion aun, registra una ";
                return View("DuracionCitasMedicion", duracionParam);
            }
            duracionParam.Id = duracionExistente.Id;
            duracionParam.duracion = duracionExistente.duracion;
            duracionParam.frecuenciaNotificaciones = duracionExistente.frecuenciaNotificaciones;
            ViewBag.message = responsePost.message;
            return View("DuracionCitasMedicion", duracionParam);
        }
    }
}
