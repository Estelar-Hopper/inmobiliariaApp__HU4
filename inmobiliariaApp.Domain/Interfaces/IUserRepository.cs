using inmobiliariaApp.Domain.Entities;

namespace inmobiliariaApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(int id);
        Task<List<User>> GetAll();
        Task<User> Add(User user);
        Task Update(User user);
        Task Delete(int id);
    }
}
