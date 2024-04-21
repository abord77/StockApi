using LearningApi.DTOs.Accounts;
using LearningApi.Interfaces;
using LearningApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

namespace LearningApi.Controllers {
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService token) {
            _userManager = userManager;
            _signInManager = signInManager;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto) { // episode 24
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) {
                return Unauthorized("Invalid username!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false); // good to not have a lockout feature enabled

            if (!result.Succeeded) {
                return Unauthorized("Username not found and/or password incorrect");
            }

            return Ok(
                new NewUserDto {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }
    }
}
