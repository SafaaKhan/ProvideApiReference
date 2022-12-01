using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvideApiReference_Models;
using ProvideApiReference_Models.DTOs;
using ProvideApiReference_Models.Helpers;
using ProvideApiReference_Models.Models;

namespace ProvideApiReference.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpGet("getUsersWithRoles")]
        [Authorize(Policy =SD.RequreAdminRolePolicy)]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var users = await _userManager.Users.
                OrderBy(x => x.DisplayName)
                .Select(u =>new
                {
                    u.Id,
                    UserName=u.UserName,
                    Roles= u.UserRoles.Select(r=>r.Role.Name).ToList()
                }).ToListAsync();

            return Ok(ResponseModel.Seccuss(User,""));
        }

        [Authorize(Policy = SD.RequreAdminRolePolicy)]
        [HttpPut("edtiRoles")]
        public async Task<IActionResult> EditRoles(EditRolesDto editRolesDto)
        {
            if (string.IsNullOrEmpty(editRolesDto.Roles))
            {
                return Ok(ResponseModel.Failure("you must select at least one roles", 400));
            }
            var selectedRoles = editRolesDto.Roles.Split(",").ToArray();

            var user = await _userManager.FindByNameAsync(editRolesDto.UserName);
            if (user == null)
            {
                return Ok(ResponseModel.Failure("the user was not found", 404));
            }

            var userRole = await _userManager.GetRolesAsync(user);
            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRole));
            //faild to add rule
            if (!result.Succeeded)
            {
                return Ok(ResponseModel.Failure("Faild to add roles", 400));
            }
            result = await _userManager.RemoveFromRolesAsync(user, userRole.Except(selectedRoles));
            if (!result.Succeeded)
            {
                return Ok(ResponseModel.Failure("Faild to remove frome roles", 400));
            }
            return Ok(ResponseModel.Seccuss(await _userManager.GetRolesAsync(user), ""));
        }


        [HttpGet("getPhotoesForModeration")]
        [Authorize(Policy =SD.ModeratePhotoRolePolicy)]
        public async Task<IActionResult> GetPhotoesForModeration()
        {
            return Ok("GetPhotoesForModeration");
        }
    }
}
