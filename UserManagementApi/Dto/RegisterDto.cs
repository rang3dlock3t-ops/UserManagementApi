using System.ComponentModel.DataAnnotations;

namespace UserManagementApi.DTO
{
    public class RegisterDto
    {
        [Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required, EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
