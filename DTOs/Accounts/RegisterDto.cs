using System.ComponentModel.DataAnnotations;

namespace LearningApi.DTOs.Accounts {
    public class RegisterDto { // from episode 22
        [Required]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        public string? Email {  get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
