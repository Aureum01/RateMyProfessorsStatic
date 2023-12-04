using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RateMyProfessorsStatic.Models
{
    public class Professor
    {
        [JsonPropertyName("Id")]
        public string? Id { get; set; }

        [JsonPropertyName("Name")]
        public string? Name { get; set; }

        [JsonPropertyName("Photo")]
        public string? Photo { get; set; }

        [JsonPropertyName("Position")]
        public string? Position { get; set; }

        [JsonPropertyName("Phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("Office")]
        public string? Office { get; set; }

        [JsonPropertyName("Ratings")]
        public Ratings? Ratings { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }

    public class Ratings
    {
        [JsonPropertyName("StarRating")]
        public double StarRating { get; set; }

        [JsonPropertyName("TopComment")]
        public List<string>? TopComment { get; set; }
    }
}