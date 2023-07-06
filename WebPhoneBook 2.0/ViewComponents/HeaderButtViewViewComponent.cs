using Microsoft.AspNetCore.Mvc;

namespace WebPhoneBook_2._0.ViewComponents
{
    public class HeaderButtViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
