using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UnqMeterAPI.DTO;
using UnqMeterAPI.JwtFeatures;
using UnqMeterAPI.Models;

namespace UnqMeterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtHandler _jwtHandler;
        public LoginController(UserManager<User> userManager, IMapper mapper, JwtHandler jwtHandler)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
        }

        [HttpPost("ExternalLogin")]
        public async Task<IActionResult> ExternalLogin([FromBody] ExternalAuth externalAuth)
        {
            try
            {
                var payload = await _jwtHandler.VerifyGoogleToken(externalAuth);
                if (payload == null)
                    return BadRequest("Invalid External Authentication.");

                var info = new UserLoginInfo(externalAuth.Provider, payload.Subject, externalAuth.Provider);

                var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(payload.Email);

                    if (user == null)
                    {
                        user = new User { Email = payload.Email, UserName = payload.Email };
                        await _userManager.CreateAsync(user);
                        await _userManager.AddLoginAsync(user, info);
                    }
                    else
                    {
                        await _userManager.AddLoginAsync(user, info);
                    }
                }

                if (user == null)
                {
                    return BadRequest("Invalid External Authentication.");
                }

                var token = await _jwtHandler.GenerateToken(user);
                return Ok(new AuthResponse { Token = token, IsAuthSuccessful = true });
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
            
        }
    }
}
