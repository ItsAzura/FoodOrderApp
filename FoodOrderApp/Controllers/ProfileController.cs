using FoodOrderApp.Data;
using FoodOrderApp.Models;
using FoodOrderApp.Models.ViewModels;
using FoodOrderApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        private readonly ApplicationDbContext _applicationDbContext;

        public ProfileController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (TempData.ContainsKey("SuccessMessage"))
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }

            var foodListVM = new FoodListViewModel
            {
                EditProfileViewModel = await BuildEditProfileViewModel()
            };
            return View(foodListVM);
        }

        private async Task<EditProfileViewModel> BuildEditProfileViewModel()
        {
            var user = await _userManager.GetUserAsync(User);

            var editProfileVM = new EditProfileViewModel();

            editProfileVM.GeneralInfo = new GeneralInfoSection();
            editProfileVM.GeneralInfo.Name = user.Name;
            editProfileVM.GeneralInfo.Email = user.Email;
            editProfileVM.GeneralInfo.PhoneNumber = user.PhoneNumber;
            editProfileVM.GeneralInfo.Address = user.Address;

            return editProfileVM;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGeneralInfo(FoodListViewModel editProfileVM)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                ModelState.Clear();
                // Kiem tra null
                if (string.IsNullOrEmpty(editProfileVM.EditProfileViewModel.GeneralInfo.Name) ||
                    string.IsNullOrEmpty(editProfileVM.EditProfileViewModel.GeneralInfo.PhoneNumber) ||
                    string.IsNullOrEmpty(editProfileVM.EditProfileViewModel.GeneralInfo.Address))
                {
                    ModelState.AddModelError("GeneralInfo", "Vui lòng điền đầy đủ thông tin!");
                    return View("Index", editProfileVM);
                }
                user.Name = editProfileVM.EditProfileViewModel.GeneralInfo.Name;
                user.PhoneNumber = editProfileVM.EditProfileViewModel.GeneralInfo.PhoneNumber;
                user.Address = editProfileVM.EditProfileViewModel.GeneralInfo.Address;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
                    return RedirectToAction("Index");
                }
            }

            return View("Index", editProfileVM);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePasswordInfo(FoodListViewModel editProfileVM)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                ModelState.Clear();
                // Kiểm tra Current Password
                var result = await _userManager.CheckPasswordAsync(user, editProfileVM.EditProfileViewModel.PasswordInfo.CurrentPassword);

                if (!result)
                {
                    ModelState.AddModelError("PasswordInfo", "Mật khẩu hiện tại không đúng!");
                    return View("Index", await BuildEditProfileViewModel());
                }

                // Kiểm tra New Password == Confirm Password
                if (editProfileVM.EditProfileViewModel.PasswordInfo.NewPassword != editProfileVM.EditProfileViewModel.PasswordInfo.ConfirmPassword)
                {
                    ModelState.AddModelError("PasswordInfo", "Mật khẩu mới và xác nhận mật khẩu không khớp!");
                    return View("Index", await BuildEditProfileViewModel());
                }

                // Đổi mật khẩu
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, editProfileVM.EditProfileViewModel.PasswordInfo.CurrentPassword, editProfileVM.EditProfileViewModel.PasswordInfo.NewPassword);

                if (changePasswordResult.Succeeded)
                {
                    TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
                    return RedirectToAction("Index");
                }

                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError("PasswordInfo", error.Description);
                    return View("Index", await BuildEditProfileViewModel());
                }
            }

            return View("Index", editProfileVM);
        }
    }
}
