using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.dtos.Account;
using backend.interfaces;
using backend.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController(UserManager<AppUser> _userManager, ITokenService _tokenService, SignInManager<AppUser> _signInManager) : ControllerBase
    {
        private readonly UserManager<AppUser> userManager = _userManager;
        private readonly ITokenService tokenService = _tokenService;
        private readonly SignInManager<AppUser> signInManager = _signInManager;

        [HttpPost("register")]
      public async Task<IActionResult> Register([FromBody] RegisterDto registerDto){
        try
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var appUser = new AppUser{
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            var createdUser = await userManager.CreateAsync(appUser, registerDto.Password);

            if(createdUser.Succeeded){
                var roleResult = await userManager.AddToRoleAsync(appUser, "User");
                if(roleResult.Succeeded){
                    return Ok(
                        new NewUserDto{
                            UserName = appUser.UserName,
                            Email = appUser.Email,
                            Token = tokenService.CreateToken(appUser)
                        }
                    );
                } else{
                    return StatusCode(500, roleResult.Errors.ToString());
                }
            } else{
                return StatusCode(500, createdUser.Errors.ToString());
            }
            
        }
        catch (Exception e)
        {
            
            return StatusCode(500, e.Message);
        }
      }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto){
        if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
        var user = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);
        if (user == null){
            return Unauthorized();
            }

        var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if(!result.Succeeded){
            return Unauthorized();
            }

        return Ok(
            new NewUserDto{
                UserName = user.UserName,
                Email = user.Email,
                Token = tokenService.CreateToken(user)
            }
        );
        } 
    }
}