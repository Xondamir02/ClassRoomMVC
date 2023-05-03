using Classroom.Data.Entities;
using ClassRoomMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClassRoomMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp([FromForm] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createUserDto);
            }

            var user = new User()
            {
                Firstname = createUserDto.Firstname,
                Lastname = createUserDto.Lastname,
                PhoneNumber = createUserDto.PhoneNumber,
                UserName = createUserDto.Username
            };
            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Username",result.Errors.First().Description);
                return View();
            }

            await _signInManager.SignInAsync(user, isPersistent: true);
            return RedirectToAction("Profile");
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInUserDto signInUserDto)
        {
            var result =
                await _signInManager.PasswordSignInAsync(signInUserDto.Username, signInUserDto.Password, true, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("User","Username or Password is incorrect");
                return View();
            }
            return RedirectToAction("Profile");
        }
    }
}
