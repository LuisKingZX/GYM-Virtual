
using Microsoft.AspNetCore.Mvc;
using DTO.Dtos;
using BL;

namespace GymFrontend.Controllers
{
    public class MedidasController : Controller
    {
        [HttpPost]
        [Route("registrar")]
        public async Task<IActionResult> registrarMedida(DTO.Dtos.MedidaCorporal medidaCorporal)
        {

            UsersManager usersManager = new UsersManager();
            ResponseHttpListadoUsers respuesta = await usersManager.obtenerUsuarios();
            List<ExistingUser> usuarios = respuesta.data;
            ViewBag.usuarios = usuarios;
            if (!ModelState.IsValid)
            {
                return View("../Users/EmpleadosMainPage", medidaCorporal);
            }
            if (medidaCorporal == null) {
                medidaCorporal = new DTO.Dtos.MedidaCorporal();
            }
            MedidasManager manager = new MedidasManager();
            var response = await manager.registrarMedida(medidaCorporal);
            ViewBag.message = response.message;
            return View("../Users/EmpleadosMainPage",medidaCorporal);
        }
    }
}
