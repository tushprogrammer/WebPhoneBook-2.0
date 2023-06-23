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
using WebPhoneBook_2._0.Entitys;
using WebPhoneBook_2._0.ContextFolder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace WebPhoneBook_2._0.Controllers
{
    public class BookController : Controller
    {
        List<Person> Persons;

        //вызов страницы
        public IActionResult Index()
        {

            //Persons = GetPersonsFromDatabase();
            ViewBag.PersonsContext = new PersonContext().Persons;
    
            IndexModel model = new IndexModel
            {
                Contacts = Persons
            };
            return View("Index", model);


        }

        //вызов страницы 
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

        [HttpDelete]
        public IActionResult DeletePerson(int id)
        {

            return View();
        }

        /// <summary>
        /// Метод получение данных из файла (уже не актуально, так как выгрузка напрямую из БД)
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