using LearningApi.Data;
using LearningApi.Mappers;
using LearningApi.DTOs.Stocks;
using Microsoft.AspNetCore.Mvc;
using LearningApi.Models;
using Microsoft.EntityFrameworkCore;
using LearningApi.Interfaces;

namespace LearningApi.Controllers {
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase { // changing all endpoints to async is episode 9
                                                    // note: anyuthing that needs to go out and find other information should be wrapped in await (ex. anything in this controller going to the db should be async)
        private readonly IStockRepository _stockRepo;
        public StockController(IStockRepository stockRepo) {
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() { // episode 4
            var stocks = await _stockRepo.GetAllAsync();
            var stockDto = stocks.Select(s => s.ToStockDTO());
            return Ok(stockDto);
        }
         
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) { // episode 4
            var stock = await _stockRepo.GetByIdAsync(id);

            if (stock == null) {
                return NotFound();
            }

            return Ok(stock);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewEntry([FromBody] CreateStockRequestDto stockDto) { // episode 6
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _stockRepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto) { // episode 7
            var stockModel = await _stockRepo.UpdateAsync(id, updateDto);

            if (stockModel == null) {
                return NotFound();
            }

            return Ok(stockModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) { // episode 8
            var stockModel = await _stockRepo.DeleteAsync(id);

            if (stockModel == null) {
                return NotFound();
            }

            return NoContent();
        }
    }
}
