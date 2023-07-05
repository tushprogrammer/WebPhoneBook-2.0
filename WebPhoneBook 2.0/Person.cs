using Newtonsoft.Json;
//using System.Text.Json.Serialization;

namespace WebPhoneBook_2._0
{
    public class Person
    {
        private int id;
        public int Id { get { return id; } set { id = value; } }

        private string name;
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get { return name; } set { name = value; } }

        private string lastname;
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get { return lastname; } set { lastname = value; } }

        private string middlename;
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get { return middlename; } set { middlename = value; } }

        private string phonenumber;
        /// <summary>
        /// Номер телефона
        /// </summary>
        public string PhoneNumber { get { return phonenumber; } set { phonenumber = value; } }

        private string address;
        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get { return address; } set { address = value; } }

        private string description;
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get { return description; } set { description = value; } }

        [JsonConstructor]
        public Person(int id, string Name, string LastName, string MiddleName, string PhoneNumber, string Address, string Description)
        {
            id = Id;
            name = Name;
            lastname = LastName;
            middlename = MiddleName;
            phonenumber = PhoneNumber;
            address = Address;
            description = Description;
        }

        public Person(string Name, string LastName, string MiddleName, string PhoneNumber, string Address, string Description)
        {
            //id уже не нужно присваивать в конструкторе, так как это произойдет автоматически при добавлении в БД
            //id = Id;
            name = Name;
            lastname = LastName;
            middlename = MiddleName;
            phonenumber = PhoneNumber;
            address = Address;
            description = Description;
        }
    }
}
