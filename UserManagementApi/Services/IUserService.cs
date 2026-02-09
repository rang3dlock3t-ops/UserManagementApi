using UserManagementApi.Models;

namespace UserManagementApi.Services
{
    public interface IUserService
    {
        public void Register(User user, string password);
    }
}
