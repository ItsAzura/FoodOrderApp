using FoodOrderApp.Data;
using FoodOrderApp.Models;
using FoodOrderApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApp.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Index()
        {
            if (TempData.ContainsKey("SuccessMessage"))
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"].ToString();
            }

            var model = await BuildEditProfileViewModel();

            return View(model);
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
        public async Task<IActionResult> UpdateGeneralInfo(EditProfileViewModel editProfileVM)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                ModelState.Clear();
                // Kiem tra null
                if (string.IsNullOrEmpty(editProfileVM.GeneralInfo.Name) ||
                    string.IsNullOrEmpty(editProfileVM.GeneralInfo.PhoneNumber) ||
                    string.IsNullOrEmpty(editProfileVM.GeneralInfo.Address))
                {
                    ModelState.AddModelError("GeneralInfo", "Vui lòng điền đầy đủ thông tin!");
                    return View("Index", editProfileVM);
                }
                user.Name = editProfileVM.GeneralInfo.Name;
                user.PhoneNumber = editProfileVM.GeneralInfo.PhoneNumber;
                user.Address = editProfileVM.GeneralInfo.Address;

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
        public async Task<IActionResult> UpdatePasswordInfo(EditProfileViewModel editProfileVM)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                ModelState.Clear();
                // Kiểm tra Current Password
                var result = await _userManager.CheckPasswordAsync(user, editProfileVM.PasswordInfo.CurrentPassword);

                if (!result)
                {
                    ModelState.AddModelError("PasswordInfo", "Mật khẩu hiện tại không đúng!");
                    return View("Index", await BuildEditProfileViewModel());
                }

                // Kiểm tra New Password == Confirm Password
                if (editProfileVM.PasswordInfo.NewPassword != editProfileVM.PasswordInfo.ConfirmPassword)
                {
                    ModelState.AddModelError("PasswordInfo", "Mật khẩu mới và xác nhận mật khẩu không khớp!");
                    return View("Index", await BuildEditProfileViewModel());
                }

                // Đổi mật khẩu
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, editProfileVM.PasswordInfo.CurrentPassword, editProfileVM.PasswordInfo.NewPassword);

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
