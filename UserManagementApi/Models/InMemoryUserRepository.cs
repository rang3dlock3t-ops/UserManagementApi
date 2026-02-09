using Microsoft.AspNetCore.Identity;
using UserManagementApi.Dto;

namespace UserManagementApi.Models
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users;


        private readonly IPasswordHasher<User> _hasher;
        public InMemoryUserRepository(IPasswordHasher<User> hasher)
        {
            _hasher = hasher;
            _users = new List<User>()
            {
                new(){UserName="Cesar",UserEmail="Admin123@gmail.com",PasswordHash= _hasher.HashPassword(null!,"123456"),Role="Admin"},
                 new(){UserName="ibdbwie",UserEmail="Rbcudecb@gmail.com",PasswordHash= _hasher.HashPassword(null!,"ioibuy#"), Role = "User"},
                 new(){UserName="ecnenco",UserEmail="123@gmail.com",PasswordHash= _hasher.HashPassword(null!,"123#4444"), Role = "User"}
            };
        
        }

        public IEnumerable<User> GetAll() => _users;

        public User? GetById(int id) => _users.FirstOrDefault(u => u.Id == id);

        public void Add(User user) => _users.Add(user);

        public void Update(int id, UpdateDto Update)
        {
            var existing = GetById(id);
            if (existing != null)
            {
                existing.UserName = Update.UserName;
                existing.UserEmail = Update.UserEmail;
            }
        }

        public void Delete(int id)
        {
            var user = GetById(id);
            if (user != null) _users.Remove(user);
        }
    }


}

