using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.MVC.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage ="kullanıcı adı girin")]
        public string Name { get; set; }
        [Required(ErrorMessage ="şifre girin")]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
