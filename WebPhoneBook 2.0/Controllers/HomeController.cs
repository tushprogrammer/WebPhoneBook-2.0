using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using WebPhoneBook_2._0.Models;

namespace WebPhoneBook_2._0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Console.WriteLine("какой нибудь текст, о том, что сработал метод index home");
            List<Person> Persons = GetPersonsFromDatabase();
            IndexModel model = new IndexModel
            {
                Contacts = Persons
            };
            return View("Index", model);
        }
        private List<Person> GetPersonsFromDatabase()
        {
            string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "People.json");
            string jsonContent = System.IO.File.ReadAllText(jsonFilePath);
            List<Person> DisplayPersons = JsonConvert.DeserializeObject<List<Person>>(jsonContent);
            return DisplayPersons;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}