using LearningApi.DTOs.Comments;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningApi.DTOs.Stocks {
    public class StockDTO {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;

        public decimal Purchase { get; set; }

        public decimal LastDiv { get; set; }

        public long MarketCap { get; set; }
        
        public List<CommentDTO> Comments { get; set; }
    }
}
