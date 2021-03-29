﻿using BinlistTestApi.Binlist.Data;
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
        public IActionResult GetCardDetails([FromBody] CardDetailsDTOW model)
        {
            _logger.LogInformation("User about to get card details ");
            try
            {
                var iin = Math.Floor(Math.Log10(model.CardNumber) + 1);
                var IIN = model.CardNumber.ToString().Count();
                var I_IN = model.CardNumber.ToString().Length;
                if(IIN < 6)
                {
                    return BadRequest(new ApiResponseDTO<string>()
                    {
                        Success = false,
                        Message = "Card Number needs to be 6 digits or 8 digits "
                    });
                }
                else if(IIN > 8)
                {
                    return BadRequest(new ApiResponseDTO<string>()
                    {
                        Success = false,
                        Message = "Card Number needs to be 6 digits or 8 digits "
                    });
                }

                //call the external servcie 
                return Ok(new
                {
                    Success = false,
                    Message = " "
                });
            }
            catch(Exception ex)
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

        [AllowAnonymous]
        [HttpGet("GetCardHits/{cardNumber}")]
        public IActionResult GetCardHits(int cardNumber)
        {
            _logger.LogInformation("User about to get card hits ");
            try
            {
                return Ok(new
                {
                    Success = false,
                    Message = "You already have a Wallet Account"
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
