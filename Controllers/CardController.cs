using BinlistTestApi.Binlist.Data;
using BinlistTestApi.Binlist.Data.Entities;
using BinlistTestApi.Helpers;
using BinlistTestApi.WriteDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinlistTestApi.Controllers
{
    [ApiController]
    [Route("api/Card")]
    public class CardController : ControllerBase
    {
        private readonly ILogger<CardController> _logger;
        private readonly ICardService _cardService;

        public CardController(ILogger<CardController> logger, ICardService cardService)
        {
            _logger = logger;
            _cardService = cardService;
        }

        
        [AllowAnonymous]
        [HttpPost("GetCardDetails")]
        public async Task<IActionResult> GetCardDetails([FromBody] CardDetailsDTOW model)
        {
            _logger.LogInformation("User about to get card details ");
            try
            {
                var iin = Math.Floor(Math.Log10(model.CardNumber) + 1);
                var IIN = model.CardNumber.ToString().Count();
                var I_IN = model.CardNumber.ToString().Length;
                if(IIN < 6)
                {
                    _logger.LogInformation("Bad Request ");
                    return BadRequest(new ApiResponseDTO<string>()
                    {
                        Success = false,
                        Message = "Card Number needs to be 6 digits or 8 digits "
                    });
                }
                else if(IIN > 8)
                {
                    _logger.LogInformation("Bad Request ");
                    return BadRequest(new ApiResponseDTO<string>()
                    {
                        Success = false,
                        Message = "Card Number needs to be 6 digits or 8 digits "
                    });
                }
                var responseStream = _cardService.GetcardDetails(model.CardNumber);

                if(responseStream == null)
                {
                    return Ok(new ApiResponseDTO<string>()
                    {
                        Success = false,
                        Message = "Something went wrong pls try again later"
                    });
                }
                var newHitCount = new HitCount
                {
                    CardNumber = model.CardNumber,
                    Count = 1
                };

                //check if card exists, if exist, increase count
                var hitCountt = _cardService.getHitCounts(model.CardNumber);
                _logger.LogInformation("Get exisitng card info ");

                if (hitCountt != null)
                {
                    hitCountt.Count += 1;
                    await _cardService.SaveChanges();

                    _logger.LogInformation("Increment count of card hits ");

                    return Ok(new ApiResponseDTO<MyRootClass>
                    {
                        Success = true,
                        Message = "Debit/Credit card Information retreived succesfully ",
                        PayLoad = responseStream
                    });
                }
                _cardService.CreateHit(newHitCount);
                await _cardService.SaveChanges();
                _logger.LogInformation("New card and count hit created ");

                return Ok(new ApiResponseDTO<MyRootClass>
                {
                    Success = true,
                    Message = "Debit/Credit card Information retreived succesfully ",
                    PayLoad = responseStream
                });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, "An exception Occured");

                return Ok(new ApiResponseDTO<string>()
                {
                    Success = true,
                    Message = "Something went wrong pls try again later"
                });

            }

        }

        [AllowAnonymous]
        [HttpGet("GetCardHits/{cardNumber}")]
        public IActionResult GetCardHits(int cardNumber)
        {
            _logger.LogInformation("User about to get card hits ");
            try
            {
                var hitCount = _cardService.getHitCounts(cardNumber);
                if(hitCount == null)
                {
                    return NotFound(new ApiResponseDTO<string>
                    {
                        Success = true,
                        Message = "Debit/Credit card Information not found, invalid IIN ",
                    });
                }
                var size = hitCount.CardNumber.ToString().Count();
                var newName = hitCount.CardNumber.ToString() + ":" + hitCount.Count.ToString();
                return Ok(new
                {
                    Success = true,
                    Size = size,
                    Response = newName
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An exception Occured");

                return Ok(new ApiResponseDTO<string>()
                {
                    Success = true,
                    Message = "Something went wrong pls try again later"
                    // Results = userDTO
                });

            }

        }
    }
}
