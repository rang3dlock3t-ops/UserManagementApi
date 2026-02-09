using System.ComponentModel.DataAnnotations;

namespace UserManagementApi.Dto
{
    public class LoginDto
    {
        [Required, EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
