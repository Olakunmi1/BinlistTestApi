using BinlistTestApi.Binlist.Data;
using BinlistTestApi.Binlist.Data.Entities;
using BinlistTestApi.Helpers;
using BinlistTestApi.ReadDTO;
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
    [ResponseCache(Duration = 60)]
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
        [ProducesResponseType(typeof(ApiResponseDTO<MyRootClass>), 200)]
        [HttpPost("GetCardDetails")]
        public async Task<IActionResult> GetCardDetails([FromBody] CardDetailsDTOW model)
        {
            _logger.LogInformation("User about to get card details ");
            try
            {
                if(model.CardNumber < 0)
                    return BadRequest(new ApiResponseDTO<string>()
                    {
                        Success = false,
                        Message = "Card Number cannot be negative "
                    });

                var IIN = model.CardNumber.ToString().Count();
                
                //optimize this into 1 line later
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
                var responseStream = await _cardService.GetcardDetails(model.CardNumber);

                if(responseStream == null)
                {
                    return NotFound(new ApiResponseDTO<string>()
                    {
                        Success = false,
                        Message = "No credit/debit details found "
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

                    _logger.LogInformation("Count of card hits Incremented ");

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

        [ProducesResponseType(typeof(ApiGenericResponseDTO<HitCountsDTO_GetAll>), 200)]
        [HttpGet("GetAllCardHits")]
        public IActionResult GetAllCardHits()
        {
            _logger.LogInformation("User about to get All card hits ");
            try
            {
                var hitCount = _cardService.getAllCardHits();
                if (hitCount == null)
                {
                    return NotFound(new ApiGenericResponseDTO<string>
                    {
                        Success = true,
                        Message = "Debit/Credit cards Information not found ",
                    });
                }
                return Ok(new ApiGenericResponseDTO<HitCountsDTO_GetAll>
                {
                    Success = true,
                    Message = "All Card hits Information ",
                    Response = hitCount
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An exception Occured");

                return Ok(new ApiGenericResponseDTO<string>()
                {
                    Success = true,
                    Message = "Something went wrong pls try again later"
                });

            }

        }

        //get Single card Hits
        /*
        [HttpGet("GetSingleCardHits/{cardNumber}")]
        public IActionResult GetSingleCardHits(int cardNumber)
        {
            _logger.LogInformation("User about to get single card hits ");
            try
            {
                var hitCount = _cardService.getHitCounts(cardNumber);
                if(hitCount == null)
                {
                    return NotFound(new ApiGenericResponseDTO<string>
                    {
                        Success = true,
                        Message = "Debit/Credit card Information not found, invalid IIN ",
                    });
                }
                var size = hitCount.CardNumber.ToString().Count();
                var newName = hitCount.CardNumber.ToString() + ":" + " " +  hitCount.Count.ToString();
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

                return Ok(new ApiGenericResponseDTO<string>()
                {
                    Success = true,
                    Message = "Something went wrong pls try again later"
                });

            }

        }
        */

    }
}
