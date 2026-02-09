using UserManagementApi.Models;
using Microsoft.AspNetCore.Identity;

namespace UserManagementApi.Services
{
    public class UserService:IUserService
    {
        private readonly IPasswordHasher<User> _Hasher;
        private readonly IUserRepository _repository;

        public UserService(IPasswordHasher<User> passwordHasher, IUserRepository repository)
        {
             _Hasher = passwordHasher;
             _repository = repository;
        }
        public void Register(User user, string password)
        {
            user.Password = _Hasher.HashPassword(user,password);
            _repository.Add(user);
        }
    }
}
