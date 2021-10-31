using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        ICardService _cardService;
        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost("add")]
        public IActionResult Add(Card card)
        {
            var result = _cardService.Add(card);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Card card)
        {
            var result = _cardService.Delete(card);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
        [HttpPost("update")]
        public IActionResult Update(Card card)
        {
            var result = _cardService.Update(card);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _cardService.GetAllCards();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbycustomerid")]
        public IActionResult GetByCustomerId(int id)
        {
            var result = _cardService.GetByCustomerId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("getbycardnumber")]
        public IActionResult GetByCardNumber(string cardNumber)
        {
            var result = _cardService.GetbyCardNumber(cardNumber);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getlistbycustomerid")]
        public IActionResult GetListByCustomerId(int customerId)
        {
            var result = _cardService.GetCardListByCustomerId(customerId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
