using CourseManagementSystem.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CourseManagementSystem.Models.Repositories
{
    public class IdentityRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public IdentityRepository(DataContext dbContext, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<ActionResult<UserDto>> SignInAsync(UserDto userDto)
        {
            try
            {
                string userName = userDto.Name.Replace(" ", String.Empty).ToLower();
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager
                    .PasswordSignInAsync(userName, userDto.Password, false, false);
                if (result.Succeeded)
                {
                    return userDto;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<ChangePasswordObj> ChangePassword(ChangePasswordObj passwordObj, ClaimsPrincipal user)
        {
            try
            {
                if (user.Identity.IsAuthenticated)
                {
                    var identityUser = await _userManager.GetUserAsync(user);
                    
                    await _userManager.ChangePasswordAsync(identityUser, passwordObj.CurrentPassword, passwordObj.NewPassword);
                    return passwordObj;
                }
                return null;
                           }
            catch(Exception)
            {
                return null;
            }
        }

        public async Task<bool> SignOutAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
