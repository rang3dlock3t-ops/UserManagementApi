using System.ComponentModel.DataAnnotations;

namespace UserManagementApi.Models
{
    public class User
    {
        public int Id { get; }

        private static int Counter = 0;

        [Required, MaxLength (50)]
        public string UserName { get; set; }
        
        [Required,EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Role = "User";


        public User()
        {
            Id = Interlocked.Increment(ref Counter);
        }
    }
}
