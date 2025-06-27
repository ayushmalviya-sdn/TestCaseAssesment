using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserOrderApp.Interface;
using UserOrderApp.Model;


namespace UserOrderApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>
        {
            new User { Id = 1, Email = "test@example.com", Password = "Test1234" },
            new User { Id = 2, Email = "john@example.com", Password = "John1234" }
        };

        public User GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public Task<bool> IsEmailRegisteredAsync(string email)
        {
            return Task.FromResult(_users.Any(u => u.Email == email));
        }
    }
}
