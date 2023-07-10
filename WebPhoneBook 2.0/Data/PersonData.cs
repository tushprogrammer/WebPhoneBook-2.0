using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
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
            List<Person> people = Context.Persons.ToList();
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
        public void EditPerson(int id, Person person)
        {
            Person personnow = Context.Persons.Where(x => x.Id == id).First();
            personnow.Name = person.Name;
            personnow.LastName = person.LastName;
            personnow.MiddleName = person.MiddleName;
            personnow.PhoneNumber = person.PhoneNumber;
            personnow.Address = person.Address;
            personnow.Description = person.Description;

            Context.SaveChanges();
        }
    }
}
