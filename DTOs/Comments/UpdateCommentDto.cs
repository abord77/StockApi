using System.ComponentModel.DataAnnotations;

namespace LearningApi.DTOs.Comments {
    public class UpdateCommentDto {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Title cannot be over 280 characters")] // data validation in viewmodel (unsure if better to do inside model)
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Content must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Content cannot be over 280 characters")]
        public string Content { get; set; } = string.Empty;
    }
}
