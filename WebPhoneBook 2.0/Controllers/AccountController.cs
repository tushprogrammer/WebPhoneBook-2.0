using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebPhoneBook_2._0.AuthPersonApp;

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
        [HttpGet]
        public IActionResult Login(string returnUrl) //открытие страницы входа
        {
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
        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserRegistration());
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
                    return RedirectToAction("Index", "Home");
                }
                else//иначе
                {
                    foreach (var identityError in createResult.Errors)
                    {
                        ModelState.AddModelError("", identityError.Description);
                    }
                }
            }

            return View(model);
        }
        #endregion

        #region Выход
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion


        public IActionResult Index() => View(_roleManager.Roles.ToList()); //вывод всех доступных ролей в системе
        public IActionResult Create() => View(); //открыть страницу создания роли

        /// <summary>
        /// Метод создания роли
        /// </summary>
        /// <param name="name">Имя новой роли</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
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
    }
}
