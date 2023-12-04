using RateMyProfessorsStatic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RateMyProfessorsStatic.Services;


namespace RateMyProfessorsStatic.Pages
{
    public class ProfessorsModel : PageModel
    {
        private readonly JsonFileProfessorService _professorService;

        public ProfessorsModel(JsonFileProfessorService professorService)
        {
            _professorService = professorService;
        }

        public IEnumerable<Professor> Professors { get; private set; } = Enumerable.Empty<Professor>();

        public void OnGet()
        {
            Professors = _professorService.GetProfessors();
        }

    }

}
