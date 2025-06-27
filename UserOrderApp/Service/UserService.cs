using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserOrderApp.Interface;
using UserOrderApp.Model;

namespace UserOrderApp.Service
{
    public class UserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo) => _repo = repo;

        public User GetUserById(int id) => _repo.GetUserById(id);
    }

}
