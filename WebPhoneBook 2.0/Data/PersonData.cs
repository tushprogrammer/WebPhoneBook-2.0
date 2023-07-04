using WebPhoneBook_2._0.ContextFolder;

namespace WebPhoneBook_2._0.Data
{
    public class PersonData : IPersonData
    {
        private readonly PersonDbContext Context;

        public PersonData(PersonDbContext context)
        {
            Context = context;
        }
        public IEnumerable<Person> GetPeople()
        {
            return Context.Persons;
        }
        public void AddPerson(Person person)
        {
            Context.Persons.Add(person);
            Context.SaveChanges();
        }
        public void RemovePerson(int id)
        {
            Person person = Context.Persons.First(p => p.Id == id);
            Context.Persons.Remove(person);
            Context.SaveChanges();
        }
    }
}
