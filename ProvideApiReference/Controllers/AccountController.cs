using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvideApiReference_Models.DTOs;
using ProvideApiReference_Models.Helpers;
using ProvideApiReference_Models.Models;
using ProvideApiReference_Utilities.Services;
using System.Security.Claims;

namespace ProvideApiReference.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;

        public AccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
            TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Ok(ResponseModel.Failure("Unauthorized user", 401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.Succeeded)
            {
                var userDto = _mapper.Map<UserDto>(user);
                userDto.Token = _tokenService.CreateToken(user);
                return Ok(ResponseModel.Seccuss(CreateUserObject(user), ""));
            }

            return Ok(ResponseModel.Failure("Unauthorized user", 401));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if(await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
            {
                return Ok(ResponseModel.Failure("Enter another email", 400));
            }
            if(await _userManager.Users.AnyAsync(x => x.UserName == registerDto.UserName))
            {
                return Ok(ResponseModel.Failure("Enter another username", 400));
            }

            var user=_mapper.Map<ApplicationUser>(registerDto);

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                return Ok(ResponseModel.Seccuss(CreateUserObject(user), "Registration successful"));
            }
            return Ok(ResponseModel.Failure("Failur in the registeration process", 400));
        }

        [Authorize]
        [HttpGet("getUser")]
        public async Task<IActionResult> GetUser()
        {
            var user = await _userManager.FindByIdAsync( User.FindFirstValue(ClaimTypes.NameIdentifier));

            return Ok(ResponseModel.Seccuss(CreateUserObject(user), "Registration successful"));
        }

        private UserDto CreateUserObject(ApplicationUser user)
        {
            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = _tokenService.CreateToken(user);
            return userDto;
        }
    }
}
