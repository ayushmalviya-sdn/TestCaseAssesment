using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserOrderApp.Model;

namespace UserOrderApp.Interface
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        Task<bool> IsEmailRegisteredAsync(string email);
    }
}
