using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Text;

namespace WebPhoneBook_2._0.Data
{
    public class PersonDataApi : IPersonData
    {
        private HttpClient httpClient { get; set; }
        static readonly string url = $@"https://localhost:7068/api/Person";

        public PersonDataApi()
        {
            httpClient = new HttpClient();
            //string url = "@" + File.ReadAllText("url.txt");
        }

        public IEnumerable<Person> GetPeople()
        {

            string json = httpClient.GetStringAsync(url).Result;

            return JsonConvert.DeserializeObject<IEnumerable<Person>>(json);

        }

        public void AddPerson(Person person)
        {
            var result = httpClient.PostAsync(url, 
                new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json" ))
                .Result;
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
