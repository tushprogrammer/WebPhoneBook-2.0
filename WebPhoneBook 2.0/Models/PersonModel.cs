using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebPhoneBook_2._0.Models
{
    public class PersonModel : PageModel
    {
        public void OnGet(string id)
        {
            Console.WriteLine("OnGet() PersonModel" + id);
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
