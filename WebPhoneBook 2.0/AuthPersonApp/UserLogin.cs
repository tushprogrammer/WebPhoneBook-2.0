using System.ComponentModel.DataAnnotations;

namespace WebPhoneBook_2._0.AuthPersonApp
{
    public class UserLogin
    {
        [Required, MaxLength(20)]
        public string LoginProp { get; set; } 

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } //пароль
        
        public string ReturnUrl { get; set; } //возвратная ссылка после авторизации
    }
}
