using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace WebPhoneBook_2._0.Models
{
    public class IndexModel : PageModel
    {

        public void OnGet()
        {
            
        }

        /// <summary>
        /// Общая БД
        /// </summary>   
        public List<Person> Contacts { get; set; }

      


    }
}
