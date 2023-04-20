using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.DTOs
{
    public class UserDto
    {
        [Required(ErrorMessage = "No te olvides del Name")]
        [JsonProperty("name")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public string Money { get; set; }
    }
}
