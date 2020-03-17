using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopAPI.Data;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationUser> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationUser> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task<ActionResult> Login(string username, string password, bool rememberMe, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await _userManager.FindByNameAsync(username);

            var result = await _signInManager.PasswordSignInAsync(user, password, rememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            return Ok();
        }

        public async Task<ActionResult> Register(string email, string password, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await _userManager.FindByEmailAsync(email);

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return LocalRedirect($"/token/{user.UserName}/{password}");
            }

            return BadRequest();
        }

        public async Task<ActionResult> ChangePassword(string username, string oldPassword, string newPassword, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return LocalRedirect(returnUrl);
            }

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            return Ok();
        }

        public async Task<ActionResult> ResetPassword(string username, string token, string password, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return LocalRedirect(returnUrl);
            }

            var result = await _userManager.ResetPasswordAsync(user, token, password);

            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            return Ok();
        }
    }
}