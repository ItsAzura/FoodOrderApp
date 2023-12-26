using FoodOrderApp.Data;
using FoodOrderApp.Helpers;
using FoodOrderApp.Models;
using FoodOrderApp.Models.ViewModels;
using FoodOrderApp.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DiaSymReader;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var response = new FoodListViewModel();
            return View(response.RegisterViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var checkEmail = registerViewModel.EmailAddress;
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == checkEmail);
            if (user != null)
            {
                ModelState.AddModelError(string.Empty, "Địa chỉ email đã tồn tại");
                return View(registerViewModel);
            }

            //  Automatically generate the UserName from the EmailAddress
            var emailParts = registerViewModel.EmailAddress.Split('@');

            var newUser = new AppUser()
            {
                Name = registerViewModel.Name,
                UserName = emailParts[0],
                Email = registerViewModel.EmailAddress,
                EmailConfirmed = true,
                PhoneNumber = registerViewModel.PhoneNumber,
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

                Cart newCart = new Cart()
                {
                    Id = CartIdGenerator.GenerateNextCartId(_context, newUser), //Lỗi trùng key khi tạo cart mới có thẻ là do cái GenerateNextCartId bị lỗi
                    AppUserId = newUser.Id,
                };

                newUser.Cart = newCart;

                await _context.Carts.AddAsync(newCart);
                await _context.SaveChangesAsync();

                await _signInManager.SignInAsync(newUser, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            //var response = new LoginViewModel();
            var response = new FoodListViewModel();
            return View(response.LoginViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(FoodListViewModel foodListViewModel)
        {
            if (!ModelState.IsValid) return View(foodListViewModel.LoginViewModel);

            var checkEmail = foodListViewModel.LoginViewModel.EmailAddress;
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == checkEmail);

            if (user != null)
            {
                //User is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, foodListViewModel.LoginViewModel.Password);
                if (passwordCheck)
                {
                    //Password correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, foodListViewModel.LoginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                //Password is incorrect
                ModelState.AddModelError(string.Empty, "Sai mật khẩu!");
                return View(foodListViewModel.LoginViewModel);
            }
            //User not found
            ModelState.AddModelError(string.Empty, "Địa chỉ email không tồn tại!");
            return View(foodListViewModel.LoginViewModel);
        }

    }
}
