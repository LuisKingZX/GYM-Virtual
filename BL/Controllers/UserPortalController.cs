//Parte hecha por Christopher Solano

using Microsoft.AspNetCore.Mvc;
using FitnessGymSolution.BL.Services;

public class UserPortalController : Controller
{
    private readonly IUserPortalService _userPortalService;

    public UserPortalController(IUserPortalService userPortalService)
    {
        _userPortalService = userPortalService;
    }

    public IActionResult Index()
    {
        return View(); // Retorna la vista principal del portal
    }

    // Métodos adicionales para manejar las acciones específicas
}