using Microsoft.AspNetCore.Mvc;
using RateMyProfessorsStatic.Services;

namespace RateMyProfessorsStatic.Controllers
{
    public class HomeController : Controller
    {
        private readonly JsonFileProfessorService _professorService;

        public HomeController(JsonFileProfessorService professorService)
        {
            _professorService = professorService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Add an action to open the modal and load the partial view data
        public IActionResult OpenModal(string professorId)
        {
            var professor = _professorService.GetProfessorById(professorId);

            if (professor == null)
            {
                return NotFound(); // Handle the case where the professor is not found
            }

            return PartialView("_ProfessorModal", professor);
        }

       

    }
}

