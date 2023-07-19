using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Text;

namespace WebPhoneBook_2._0.Data
{
    public class PersonDataApi : IPersonData
    {
        private HttpClient httpClient { get; set; }
        static readonly string url = $@"api/Person";

        public PersonDataApi()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(@"https://localhost:7068/");
            //string url = "@" + File.ReadAllText("url.txt");
        }

        public IEnumerable<Person> GetPeople()
        {

            string json = httpClient.GetStringAsync(url).Result;
            return JsonConvert.DeserializeObject<IEnumerable<Person>>(json);

        }

        public void AddPerson(Person person)
        {
            var re = httpClient.PostAsJsonAsync(url, person).Result;
            //тоже самое, но без кодировки и типа отправляемых данных
            //var result = httpClient.PostAsync(url, 
            //    new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json" ))
            //    .Result;
        }
        public void RemovePerson(int id)
        {
            var result = httpClient.DeleteAsync($"{url}/{id}").Result; 
        }
        public void EditPerson(int id,  Person person)
        {
            var result = httpClient.PutAsync($"{url}/{id}", new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json"))
                .Result;
        }
    }
}
