namespace LearningApi.DTOs.Comments {
    public class CreateCommentDto { // different naming convention compared to CreateStockRequestDto (can choose whichever is better later)
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
