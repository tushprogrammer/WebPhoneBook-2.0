using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using WebPhoneBook_2._0.AuthPersonApp;
using WebPhoneBook_2._0.Models;

namespace WebPhoneBook_2._0.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        #region Вход
        [HttpGet, AllowAnonymous]
        public IActionResult Login(string returnUrl) //открытие страницы входа
        {
            if (returnUrl is null)
            {
                returnUrl = "/Book/Index";
            }
            return View(new UserLogin()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin model) // обработка введенных данных на странице входа
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(model.LoginProp,
                    model.Password,
                    false,
                    lockoutOnFailure: false); //попытка найти в бд пользователя с введенными логином и паролем

                if (loginResult.Succeeded) //если попытка успешна
                {
                    if (Url.IsLocalUrl(model.ReturnUrl)) // если при входе была переадресация с другой страницы
                    {
                        return Redirect(model.ReturnUrl); //возврат на исходную страницу
                    }

                    return RedirectToAction("Index", "Book"); // иначе возврат на стартовую страницу
                }

            }
            
            ModelState.AddModelError("", "Пользователь не найден");
            return View(model); //если не найден, повторная попытка
        }
        #endregion

        #region Регистрация 
        [HttpGet, AllowAnonymous]
        public IActionResult Register()
        {
            return View("Registration", new UserRegistration());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistration model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.LoginProp };
                var createResult = await _userManager.CreateAsync(user, model.Password);

                if (createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Book");
                }
                else //если регистрация не удалась
                {
                    foreach (var identityError in createResult.Errors)
                    {
                        ModelState.AddModelError("", identityError.Description);
                    }
                }
            }

            return View("Registration",model);
        }
        #endregion

        #region Выход
        //[HttpPost, ValidateAntiForgeryToken]
        //public IActionResult Logout()
        //{
        //    _signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "Book");
        //}
        [HttpPost, ValidateAntiForgeryToken, Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Book");
        }
        #endregion

        [Authorize(Roles = "Admin")]
        public IActionResult Index() //вывод всех доступных ролей в системе 
        {
            return View(_roleManager.Roles.ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UserList() //список пользователей
        {
            return View("Users",_userManager.Users.ToList());
        }

        #region Редактирование ролей у пользователя
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(string userId) //окно редактирования ролей у пользователя
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles, //уже имеющиекся роли пользователя
                    AllRoles = allRoles //все роли
                };
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string userId, List<string> roles)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которых у пользователя изначально не было
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые у пользователя убрали
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }
        #endregion

        #region Создание роли
        
        [Authorize(Roles = "Admin")]
        public IActionResult CreateRole() //открыть страницу создания роли
        {
            return View();
        }
        


        /// <summary>
        /// Метод создания роли
        /// </summary>
        /// <param name="name">Имя новой роли</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        #endregion

        #region Удаление пользователя
        public async Task<IActionResult> DeleteUser(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                _userManager.DeleteAsync(user);
            }
            return RedirectToAction("UserList");
        }
        #endregion

    }
}
