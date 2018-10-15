using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gruppo3Esame.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gruppo3Esame.Controllers
{
    public class AuthenticationController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public AuthenticationController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login viewModel)
        {
            if (viewModel == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(viewModel);

            // The UserManager purpose is to handle users,
            // creating them, retrieving them, checking/changing passwords, etc.
            var user = await _userManager.FindByEmailAsync(viewModel.Email);
            if (user != null)
            {
                var checkPwd = await _userManager.CheckPasswordAsync(user, viewModel.Password);

                if (checkPwd)
                {
                    // The SignInManager just signs users in and out
                    // and keeps track of the logged users.
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("index", "home");
                }
            }

            ModelState.AddModelError("", "Invalid email and/or password!");
            return View(viewModel);
        }

        [HttpPost]
        [Authorize] // an action with this attribute is accessible only by authenticated users.
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login");
        }

        // This is just a method to create a default user, so we can test authentication.
        // NEVER do this in a normal project.
        // Or you have a page to create a user, or you have the list of users
        // loaded in the startup from some source.
        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            var user = new IdentityUser
            {
                UserName = "default.user",
                Email = "q@w.e",
            };

            var result = await _userManager.CreateAsync(user, "qweqwe");
            if (result.Succeeded)
            {
                return RedirectToAction("login", "authentication");
            }
            else
            {
                return new JsonResult(result.Errors);
            }
        }
    }
}