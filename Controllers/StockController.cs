using LearningApi.Data;
using LearningApi.Mappers;
using LearningApi.DTOs.Stocks;
using Microsoft.AspNetCore.Mvc;
using LearningApi.Models;

namespace LearningApi.Controllers {
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase {
        private readonly ApplicationDBContext _dbContext;
        public StockController(ApplicationDBContext dbContext) {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll() { // ep 4
            var stock = _dbContext.Stocks.ToList().Select(s => s.ToStockDTO());
            return Ok(stock);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id) { // ep 4
            var stock = _dbContext.Stocks.Find(id);

            if (stock == null) {
                return NotFound();
            }
            return Ok(stock.ToStockDTO());
        }

        [HttpPost]
        public IActionResult CreateNewEntry([FromBody] CreateStockRequestDto stockDto) { // ep 6
            var stockModel = stockDto.ToStockFromCreateDTO();
            _dbContext.Stocks.Add(stockModel);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto) { // ep 7
            var stockModel = _dbContext.Stocks.FirstOrDefault(x => x.Id == id);

            if (stockModel == null) {
                return NotFound();
            }


            // this takes the payload to this endpoint and modifies the id using that body
            stockModel.Symbol = updateDto.Symbol;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;
            stockModel.MarketCap = updateDto.MarketCap;

            _dbContext.SaveChanges();
            return Ok(stockModel.ToStockDTO());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id) { // ep 8
            var stockModel = _dbContext.Stocks.FirstOrDefault(x => x.Id == id);

            if (stockModel == null) {
                return NotFound();
            }

            _dbContext.Stocks.Remove(stockModel);
            _dbContext.SaveChanges();
            return NoContent();
        }
    }
}
