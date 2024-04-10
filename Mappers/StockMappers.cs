﻿using LearningApi.DTOs.Stocks;
using LearningApi.Models;

namespace LearningApi.Mappers {
    public static class StockMappers {
        public static StockDTO ToStockDTO(this Stock stockModel) {
            return new StockDTO {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap
            };
        }
    }
}
