using System.ComponentModel.DataAnnotations;

namespace Imtahan.ViewModels.Users
{
    public class RegisterVM
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}