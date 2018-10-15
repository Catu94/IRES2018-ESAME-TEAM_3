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
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(Registration viewModel)
        {
            if (viewModel == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(viewModel);

            var user = await _userManager.FindByEmailAsync(viewModel.Email);
            if (user == null)
            {
                var newUser = new IdentityUser
                {
                    UserName = viewModel.Name,
                    Email = viewModel.Email,
                };

                var result = await _userManager.CreateAsync(newUser, viewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("login", "Authentication");
                }
                else
                {
                    return new JsonResult(result.Errors);
                }
            }

            ModelState.AddModelError("", "User already exists!");
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            TempData["Authenticated"] = false;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login viewModel)
        {
            if (viewModel == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(viewModel);

            var user = await _userManager.FindByEmailAsync(viewModel.Email);
            if (user != null)
            {
                var checkPwd = await _userManager.CheckPasswordAsync(user, viewModel.Password);

                if (checkPwd)
                {
                    await _signInManager.SignInAsync(user, false);
                    TempData["Authenticated"] = true;
                    return RedirectToAction("index", "home");
                }
            }

            ModelState.AddModelError("", "Invalid email and/or password!");
            return View(viewModel);
        }
     
        [Authorize] 
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["Authenticated"] = false;
            return RedirectToAction("login");
        }
       
    }
}