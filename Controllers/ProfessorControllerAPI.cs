using Microsoft.AspNetCore.Mvc;
using RateMyProfessorsStatic.Models;
using RateMyProfessorsStatic.Services;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RateMyProfessorsStatic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorControllerAPI : ControllerBase
    {
        private readonly JsonFileProfessorService _professorService;

        public ProfessorControllerAPI(JsonFileProfessorService professorService)
        {
            _professorService = professorService;
        }

        // GET: api/<ProfessorControllerAPI>
        [HttpGet]
        public ActionResult<IEnumerable<Professor>> Get()
        {
            var professors = _professorService.GetProfessors();
            return Ok(professors); // Return all professors
        }

        // GET api/<ProfessorControllerAPI>/5
        [HttpGet("{id}")]
        public ActionResult<Professor> Get(int id)
        {
            //id as string since the professor id is a string id 
            var professor = _professorService.GetProfessors().FirstOrDefault(p => p.Id == id.ToString());

            if (professor == null)
            {
                return NotFound(); // return 404 if not found
            }

            return Ok(professor); // return specific professor
        }

        // GET api/<ProfessorControllerAPI>/search?name={name}
        [HttpGet("query")]
        public ActionResult<IEnumerable<Professor>> Search(string name)
        {
            var results = string.IsNullOrWhiteSpace(name)
                ? Enumerable.Empty<Professor>()
                : _professorService.GetProfessors()
                    .Where(p => !string.IsNullOrEmpty(p.Name) && p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            return Ok(results);
        }



    }
}
