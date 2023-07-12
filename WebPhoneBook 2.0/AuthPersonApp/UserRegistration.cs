using System.ComponentModel.DataAnnotations;

namespace WebPhoneBook_2._0.AuthPersonApp
{
    public class UserRegistration
    {
        [Required, MaxLength(20)]
        public string LoginProp { get; set; } 

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public bool IsAdmin { get; set; }
    }
}
