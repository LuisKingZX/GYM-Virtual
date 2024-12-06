using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Identity;
using BL;
using BL.Services;
using DTO.Dtos;
namespace GymFrontend.Controllers
{
    public class UsersController : Controller
    {
        [HttpGet]
        [Route("signIn")]
        public IActionResult Index()
        {
            return View("Login", new UserCredentials());
        }

        [HttpPost]
        [Route("signIn")]
        public IActionResult executeSignIn(UserCredentials credenciales)
        {
            if (!ModelState.IsValid) { 
                return View("Login",credenciales);
            }

            UsersManager manager = new UsersManager();
            HttpLoginResponse respuesta = manager.login(credenciales).Result;
            if (respuesta.ok == false) {
                ViewBag.message = "Usuario y/o contraseña incorrectos";
                return View("Login");
            }
			ViewBag.message = "";
            ViewBag.cel = respuesta.data.Telefono;
            ViewBag.correo = respuesta.data.Correo;
            //return Redirect("https://localhost:7275/empleados");
            return Redirect(respuesta.urlPageForUser+"?celular="+respuesta.data.Telefono+"&email="+respuesta.data.Correo);
        }

        [HttpGet]
        [Route("signUp")]
        public IActionResult crearCuenta()
        {
            return View();
        }

        [HttpPost]
        [Route("signUp")]
        public IActionResult postCrearCuenta(User usuario)
        {
			if (!ModelState.IsValid)
			{
				return View("CrearCuenta",usuario);
			}
			UsersManager manager = new UsersManager();
			ResponseHttp respuesta = manager.registrarUsuario(usuario).Result;
			ViewBag.message = respuesta.message;
            return View("CrearCuenta");
		}

		[HttpGet]
        [Route("clientes")]
        public IActionResult dirigirACrearPaginaDeClientes()
        {
            
            return View("ClientesMainPage");
        }

        [HttpGet]
        [Route("empleados")]
        public IActionResult dirigirACrearPaginaDeEmpelados()
        {
            UsersManager manager = new UsersManager();
            List<ExistingUser> usuarios = manager.obtenerUsuarios().Result.data;
            ViewBag.usuarios = usuarios;
            return View("EmpleadosMainPage");
        }


        [HttpGet]
        [Route("validacionOTP")]
        public IActionResult dirigirACrearPaginaValidacionOTP([FromQuery]string celular, [FromQuery] string email)
        {
            celular = celular.Trim();
            email = email.Trim();
            UsersManager manager = new UsersManager();
            OTPCodeVerification verificacion = new OTPCodeVerification() { cel = "+"+celular, correo = email };
            CanalDeEnvioOTP canal = new CanalDeEnvioOTP() {cel = "+"+celular,correo=email };
            HttpResponseGenerateOTP response = manager.obtenerCodigoOTP(canal).Result;
            return View("ValidacionOTP", verificacion);
        }

        [HttpPost]
        [Route("validacionOTPPost")]
        public IActionResult pedirValidacionDeCodigoOTP(OTPCodeVerification verificacion)
        {
            UsersManager manager = new UsersManager();
            HttpLoginResponse response = manager.validarCodigoOTP(verificacion).Result;
            
            return Redirect(response.urlPageForUser);
        }

		[HttpGet]
        [Route("listausuarios")]
        public IActionResult dirigirAListaDeUsuarios()
        {
            UsersManager manager = new UsersManager();
            List<ExistingUser> usuarios = manager.obtenerUsuarios().Result.data;
            ViewBag.usuarios = usuarios;
            return View("ListUsuarios");
        }
    }
}
