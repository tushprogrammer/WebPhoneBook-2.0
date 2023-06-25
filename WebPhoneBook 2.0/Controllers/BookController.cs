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
using Microsoft.AspNetCore.Mvc.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebPhoneBook_2._0.Controllers
{
    public class BookController : Controller
    {
        List<Person> Persons;

        //вызов страницы
        public IActionResult Index()
        {
            ViewBag.PersonsContext = new PersonContext().Persons;
            return View();
        }

        //вызов страницы 
        public IActionResult Person(string id)
        {
            int h_id = Convert.ToInt32(id);
            Persons = new PersonContext().Persons.ToList();
            PersonModel personModel = new PersonModel
            {
                Person = Persons[h_id - 1]
            };
            return View("Person", personModel);
        }
        //вызов страницы
        public IActionResult PersonDetails()
        {

            return View();
        }
        //вызов страницы
        public IActionResult AddPerson()
        {
            return View();
        }
        public IActionResult EditPerson(int id)
        {
            Persons = new PersonContext().Persons.ToList();
            ViewBag.PersonNow = Persons[id - 1];
            return View();
        }
        /// <summary>
        /// Метод добавления нового контакта
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="LastName"></param>
        /// <param name="MiddleName"></param>
        /// <param name="PhoneNumber"></param>
        /// <param name="Address"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        public IActionResult AddNewPerson(string Name, string LastName, string MiddleName, string PhoneNumber, string Address, string Description)
        {
            using (PersonContext context = new PersonContext())
            {
                
                Person NewPerson = new Person(Name, LastName, MiddleName, PhoneNumber, Address, Description);
                context.Add(NewPerson);
                context.SaveChanges();
            }
            return Redirect("~/"); //возврат к первой странице
        }

        /// <summary>
        /// Метод обработки кнопки удаления контакта
        /// </summary>
        /// <param name="id">Идентификатор контакта</param>
        /// <returns></returns>
        //[HttpDelete("{id}")]
        [HttpGet]
        public IActionResult DeletePerson(int id)
        {
            //по хорошему сделать отдельный класс, где будет логика, и там уже 
            using (PersonContext newcontext = new PersonContext())
            {
                Person PersonDelete = newcontext.Persons.Where(x => x.Id == id).First();
                newcontext.Persons.Remove(PersonDelete);
                newcontext.SaveChanges();
            }
            //return View(); //после удаления, обновление страницы
            return Ok(); //возврат статуса 200
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