﻿using System.ComponentModel.DataAnnotations;

namespace LearningApi.DTOs.Stocks {
    public class CreateStockRequestDto {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters")] // episode 17 for data validation
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MaxLength(20, ErrorMessage = "Company name cannot be over 20 characters")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20, ErrorMessage = "Industry name cannot be over 20 characters")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(1, 5000000000000)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }

        [Required]
        [Range(1, 5000000000000)]
        public long MarketCap { get; set; }
    }
}
