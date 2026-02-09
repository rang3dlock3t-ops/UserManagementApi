using UserManagementApi.Dto;

namespace UserManagementApi.Models
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User? GetById(int id);
        void Add(User user);
        void Update(int id,UpdateDto user);
        void Delete(int id);

    }
}
