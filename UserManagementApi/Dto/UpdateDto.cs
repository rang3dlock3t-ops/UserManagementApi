using System.ComponentModel.DataAnnotations;

namespace UserManagementApi.Dto
{
    public class UpdateDto
    {
        [Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required, EmailAddress]
        public string UserEmail { get; set; }
    }
}
