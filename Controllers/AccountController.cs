using BinlistTestApi.Binlist.Data.Entities;
using BinlistTestApi.Helpers;
using BinlistTestApi.ReadDTO;
using BinlistTestApi.WriteDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BinlistTestApi.Controllers
{
    [ApiController]
    [Route("api/Account")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private IConfiguration _Config { get; }

        public AccountController(ILogger<AccountController> logger, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _logger = logger;
            _userManager = userManager;
            _Config = config;
        }

        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponseDTO<SystemUserDTO>), 200)]
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(SystemuserDTOW model)
        {         
            try
            {
                _logger.LogInformation("User about to register");

                StringBuilder strbld2 = new StringBuilder();
                var err2 = new List<string>();
                if (!ModelState.IsValid)
                {
                    foreach (var state in ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            err2.Add(error.ErrorMessage);
                            err2.ForEach(err => { strbld2.AppendFormat("•{0}", error.ErrorMessage); });
                        }
                    }


                    return BadRequest(new { message = strbld2 });
                }
                //create Account
                var user = new ApplicationUser { UserName = model.UserName, Email = model.email};

                IdentityResult result = await _userManager.CreateAsync(user, model.password);

                if (!result.Succeeded)
                {
                    var errors = result.Errors;
                    var message = string.Join(", ", errors.Select(x => x.Code + "," + " " + x.Description));
                 
                    _logger.LogDebug(message, "Bad Request");
                    return BadRequest(new ApiResponseDTO<string> { Success = false, Message = message });
                }
                var userDTO = new SystemUserDTO
                {
                    email = model.email
                };

                return Ok(new ApiResponseDTO<SystemUserDTO>()
                {
                    Success = true,
                    Message = "Registration was Succesful, below is your email.",
                    PayLoad = userDTO
                });
            }

            catch (Exception ex)
            {
                var messg = ex.Message;
                _logger.LogError(messg, "An exception Occured ");
                return Ok(new ApiResponseDTO<string> 
                {
                    Success = false,
                    Message = "Something went wrong pls try again later"
                });
            }
        }

        //Login and grab a token 
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponseDTO<TokenStructureDTO>), 200)]
        [HttpPost("Authenticate/token")]
        public async Task<IActionResult> Authenticate([FromBody] Authenticate model)
        {
            try
            {

                _logger.LogInformation("User About to login and get a token");

                StringBuilder strbld2 = new StringBuilder();
                var err2 = new List<string>();
                if (!ModelState.IsValid)
                {
                    foreach (var state in ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            err2.Add(error.ErrorMessage);
                            err2.ForEach(err => { strbld2.AppendFormat("•{0}", error.ErrorMessage); });
                        }
                    }

                    return BadRequest(new { message = strbld2 });
                }

                var user = await _userManager.FindByEmailAsync(model.email);
                if (user != null && (await _userManager.CheckPasswordAsync(user, model.Password)))
                {

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(_Config["AppSettings:Secret"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(JwtRegisteredClaimNames.Sub, model.email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    var tokenString = tokenHandler.WriteToken(token);
                    var expiresWhen = tokenDescriptor.Expires;

                    // return basic user info and authentication token
                    _logger.LogInformation("Returned Token succesfully");
                    var tokenDetails = new TokenStructureDTO
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        TokenString = tokenString,
                        ExpiresWhen = expiresWhen
                    };

                    return Ok(new ApiResponseDTO<TokenStructureDTO>
                    {
                        Success = true,
                        Message = "Returned Token succesfully",
                        PayLoad = tokenDetails
                    });

                }
                _logger.LogDebug("Email or password is incorrect");

                return Ok(new ApiResponseDTO<string>
                {
                    Success = false,
                    Message = "Email or password is incorrect"
                });
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An exception Occured");
                return Ok(new ApiResponseDTO<string>
                {
                    Success = false,
                    Message = "Something went wrong pls try again later"
                });
            }
        }
    }
}
