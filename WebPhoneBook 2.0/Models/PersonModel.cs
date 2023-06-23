using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebPhoneBook_2._0.Models
{
    public class PersonModel : PageModel
    {
        public void OnGet()
        {
            //текст сам не выводится в консоль, но без этого текста не открывается новая страница
            Console.WriteLine("OnGet() PersonModel");
        }
        /// <summary>
        /// Конкретный человек, которого открыли
        /// </summary>
        public Person Person { get; set; } 
        /// <summary>
        /// Общая БД
        /// </summary>
        public List<Person> Contacts { get; set; }
    }
}
