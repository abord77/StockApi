using LearningApi.DTOs.Accounts;
using LearningApi.Interfaces;
using LearningApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace LearningApi.Controllers {
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager, ITokenService token) {
            _userManager = userManager;
            _tokenService = token;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto) { // episode 22
            try {
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }

                var appUser = new AppUser { 
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded) {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User"); // giving anyone through this endpoint to be a user
                    if (roleResult.Succeeded) {
                        return Ok(
                            new NewUserDto {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            }
                        );
                    } else {
                        return StatusCode(500, roleResult.Errors);
                    }
                } else {
                    return StatusCode(500, createdUser.Errors);
                }
            } catch (Exception ex) {
                return StatusCode(500, ex); // in case the 2 cases above fail, this will catch and return that error too
            }
        }
    }
}
