using RateMyProfessorsStatic.Models;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace RateMyProfessorsStatic.Services
{
    public class JsonFileProfessorService
    {
        private readonly ILogger<JsonFileProfessorService> _logger;

        public JsonFileProfessorService(IWebHostEnvironment webHostEnvironment, ILogger<JsonFileProfessorService> logger)
        {
            WebHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public Professor GetProfessorById(string professorId)
        {
            var professors = GetProfessors();
            var professor = professors.FirstOrDefault(p => p.Id == professorId);
            return professor ?? GetDefaultProfessor(); // Return a default professor if null
        }

        private Professor GetDefaultProfessor()
        {
            return new Professor(); // Create and return a default professor
        }



        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "names.json");

        public IEnumerable<Professor> GetProfessors()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            var professors = JsonSerializer.Deserialize<Professor[]>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return professors ?? Enumerable.Empty<Professor>(); // Avoid returning null
        }

        public void AddRating(string professorId, double newRating)
        {
            var professors = GetProfessors().ToList();
            var professor = professors.FirstOrDefault(x => x.Id == professorId);

            if (professor != null)
            {
                if (professor.Ratings == null)
                {
                    professor.Ratings = new Ratings
                    {
                        StarRating = newRating,
                        TopComment = new List<string>() // Initialize if null
                    };
                }
                else
                {
                    var totalRatings = professor.Ratings.StarRating * (professor.Ratings.TopComment?.Count ?? 0);
                    totalRatings += newRating;
                    var newCount = (professor.Ratings.TopComment?.Count ?? 0) + 1;

                    if (newCount > 0) // Ensure newCount is not zero
                    {
                        professor.Ratings.StarRating = totalRatings / newCount;
                    }
                }

                // Overwrite the JSON file with updated data
                File.WriteAllText(JsonFileName, JsonSerializer.Serialize(professors, new JsonSerializerOptions { WriteIndented = true }));
            }
            else
            {
                _logger.LogError($"Professor with ID {professorId} not found.");
                throw new KeyNotFoundException($"Professor with ID {professorId} not found.");
            }
        }
    }
}
