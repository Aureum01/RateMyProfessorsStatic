using Microsoft.AspNetCore.Mvc;
using RateMyProfessorsStatic.Models;
using RateMyProfessorsStatic.Services;
using System.Collections.Generic;
using System.Linq;

namespace RateMyProfessorsStatic.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly JsonFileProfessorService _professorService;

        public ProfessorController(JsonFileProfessorService professorService)
        {
            _professorService = professorService;
        }

        public IActionResult Results(string query)
        {
            IEnumerable<Professor> results;

            if (string.IsNullOrWhiteSpace(query))
            {
                results = Enumerable.Empty<Professor>();
            }
            else
            {
                results = _professorService.GetProfessors()
                            .Where(p => !string.IsNullOrEmpty(p.Name) && p.Name.Contains(query, StringComparison.OrdinalIgnoreCase));
            }

            return View(results);
        }

        [HttpGet("search")]
        public IActionResult Search(string term)
        {
            var results = string.IsNullOrWhiteSpace(term)
                ? Enumerable.Empty<Professor>()
                : _professorService.GetProfessors()
                    .Where(p => !string.IsNullOrEmpty(p.Name) && p.Name.StartsWith(term, StringComparison.OrdinalIgnoreCase))
                    .Take(10); // Limit the number of results

            return Json(results);
        }
    }
}
