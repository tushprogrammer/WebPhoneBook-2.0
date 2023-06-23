using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebPhoneBook_2._0.Models
{
    public class PersonModel : PageModel
    {
        public void OnGet()
        {
            //����� ��� �� ��������� � �������, �� ��� ����� ������ �� ����������� ����� ��������
            Console.WriteLine("OnGet() PersonModel");
        }
        /// <summary>
        /// ���������� �������, �������� �������
        /// </summary>
        public Person Person { get; set; } 
        /// <summary>
        /// ����� ��
        /// </summary>
        public List<Person> Contacts { get; set; }
    }
}
