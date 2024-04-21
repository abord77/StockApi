using System.ComponentModel.DataAnnotations;

namespace LearningApi.DTOs.Accounts {
    public class LoginDto {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
