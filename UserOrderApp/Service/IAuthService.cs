using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserOrderApp.Service
{
    public interface IAuthService
    {
        bool IsValidPassword(string password);
        Task<bool> IsEmailRegisteredAsync(string email);
    }
}
