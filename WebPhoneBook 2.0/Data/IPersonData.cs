namespace WebPhoneBook_2._0.Data
{
    public interface IPersonData
    {
        IEnumerable<Person> GetPeople();
        void AddPerson(Person person);
        void RemovePerson(int id);
    }
}
