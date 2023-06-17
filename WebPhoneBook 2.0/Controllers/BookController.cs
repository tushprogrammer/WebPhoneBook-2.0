using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebPhoneBook_2._0.Models;

namespace WebPhoneBook_2._0.Controllers
{
    public class BookController : Controller
    {
        List<Person> Persons;
        public IActionResult Index()
        {
            
            Persons = GetPersonsFromDatabase();
            IndexModel model = new IndexModel
            {
                Contacts = Persons
            };
            return View("Index", model);


        }

        public IActionResult Person(string id)
        {
            int h_id = Convert.ToInt32(id);
            Persons = GetPersonsFromDatabase();
            PersonModel personModel = new PersonModel
            {
                Contacts = Persons,
                Person = Persons[h_id - 1]
            };
            return View("Person", personModel);
        }

        /// <summary>
        /// Метод получение данных из файла
        /// </summary>
        /// <returns></returns>
        private List<Person> GetPersonsFromDatabase()
        {
            string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "People.json");
            string jsonContent = System.IO.File.ReadAllText(jsonFilePath);
            List<Person> DisplayPersons = JsonConvert.DeserializeObject<List<Person>>(jsonContent);
            return DisplayPersons;
        }
      

       
    }
}