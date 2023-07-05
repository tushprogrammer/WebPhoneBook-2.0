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
using WebPhoneBook_2._0.Data;
using Microsoft.AspNetCore.Authorization;

namespace WebPhoneBook_2._0.Controllers
{
    public class BookController : Controller
    {
        //List<Person> Persons;
        private readonly IPersonData Persons;
        public BookController(IPersonData persons)
        {
            Persons = persons;
        }

        //вызов базовой страницы
        [AllowAnonymous]
        public IActionResult Index()
        {
            List<Person> people = Persons.GetPeople().ToList();
            ViewBag.PersonsContext = people;
            return View();
        }

        //вызов страницы контакта 
        public IActionResult Person(string id)
        {
            int h_id = Convert.ToInt32(id) - 1;
            IEnumerable<Person> People = Persons.GetPeople();
            PersonModel personModel = new PersonModel
            {
                Person = People.First(Person => Person.Id == h_id),
            };
            return View("Person", personModel);
        }
        //вызов страницы добавления контакта
        public IActionResult AddPerson()
        {
            return View();
        }
        //вызов страницы изменения контакта
        public IActionResult EditPerson(int id)
        {
            IEnumerable<Person> People = Persons.GetPeople();
            Person personNow
                = People.First(Person => Person.Id == id);
            ViewBag.PersonNow = personNow;
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
        public IActionResult AddNewPerson(string Name, string LastName, string MiddleName, 
            string PhoneNumber, string Address, string Description)
        {
            Person NewPerson = new Person(Name, LastName, MiddleName,
                PhoneNumber, Address, Description);
            Persons.AddPerson(NewPerson);

            return Redirect("~/"); //возврат к первой странице
        }

        /// <summary>
        /// Метод изменения данных контакта
        /// </summary>
        /// <param name="id"></param>
        /// <param name="NewName"></param>
        /// <param name="NewLastName"></param>
        /// <param name="NewMiddleName"></param>
        /// <param name="NewPhoneNumber"></param>
        /// <param name="NewAddress"></param>
        /// <param name="NewDescription"></param>
        /// <returns></returns>
        public IActionResult EditPersonContext(int id, string NewName, string NewLastName, string NewMiddleName,
            string NewPhoneNumber, string NewAddress, string NewDescription) 
        {
            //
            //using (PersonDbContext context = new PersonDbContext())
            //{
            //    Person PersonNow = context.Persons.Where(x => x.Id == id).First();
            //    PersonNow.Name = NewName;
            //    PersonNow.LastName = NewLastName;
            //    PersonNow.MiddleName = NewMiddleName;
            //    PersonNow.PhoneNumber = NewPhoneNumber;
            //    PersonNow.Address = NewAddress;
            //    PersonNow.Description = NewDescription;
            //    context.SaveChanges();
            //}
            
            return Redirect("~/"); //возврат к главной странице
        }
        /// <summary>
        /// Обработка кнопки удаления контакта
        /// </summary>
        /// <param name="id">Идентификатор контакта</param>
        /// <returns></returns>
        //[HttpDelete("{id}")]
        [HttpGet]
        public IActionResult DeletePerson(int id)
        {
            DeletePersonContext(id);
            return Redirect("~/"); //обновление главной страницы
        }
        /// <summary>
        /// Метод удаления конкретного контакта
        /// </summary>
        /// <param name="id"></param>
        private void DeletePersonContext(int id)
        {
            Persons.RemovePerson(id);
        }
    }
}