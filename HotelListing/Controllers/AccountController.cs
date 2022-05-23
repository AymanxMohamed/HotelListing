using AutoMapper;
using HotelListing.Data;
using HotelListing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;

        public AccountController(UserManager<ApiUser> userManager, 
            ILogger<AccountController> logger, 
            IMapper mapper)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            _logger.LogInformation($"Registration Attempt for {userDTO.Email}");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var user = _mapper.Map<ApiUser>(userDTO);
                user.UserName = userDTO.Email;
                var result = await _userManager.CreateAsync(user, userDTO.Password);
     
                if (!result.Succeeded)
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(err.Code, err.Description);
                    }
                    return BadRequest(ModelState);
                }
                return Accepted();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Somthing Went Wrong in the {nameof(Register)}");
                return Problem($"Somthing Went Wrong in the {nameof(Register)}", statusCode: 500);
            }
        }

        //[HttpPost]
        //[Route("login")]
        //[ProducesResponseType(StatusCodes.Status202Accepted)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
        //{
        //    _logger.LogInformation($"Login Attempt for {userDTO.Email}");
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    try
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password, false, false);
        //        if (!result.Succeeded)
        //            return Unauthorized(userDTO);
        //        return Accepted();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Somthing Went Wrong in the {nameof(Login)}");
        //        return Problem($"Somthing Went Wrong in the {nameof(Login)}", statusCode: 500);
        //    }
        //}
    }
}
