namespace WebPhoneBook_2._0.Data
{
    public class PersonDataApi : IPersonData
    {
        private HttpClient httpClient { get; set; }

        public PersonDataApi()
        {
            httpClient = new HttpClient();
        }

            //public IEnumerable<Person> GetPeople()
            //{
            //    string url = @"https://localhost:44307/api/values";

            //}
    }
}
