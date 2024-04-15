﻿using LearningApi.Models;

namespace LearningApi.DTOs.Comments {
    public class CommentDTO {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockId { get; set; } 
    }
}
