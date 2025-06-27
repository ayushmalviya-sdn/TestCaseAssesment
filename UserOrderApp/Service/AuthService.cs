using System.Linq;
using System.Threading.Tasks;
using UserOrderApp.Interface;

namespace UserOrderApp.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;

        public AuthService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> IsEmailRegisteredAsync(string email)
        {
            return await _repo.IsEmailRegisteredAsync(email);
        }

        public bool IsValidPassword(string password)
        {
            if (password.Length < 8) return false;
            if (!password.Any(char.IsUpper)) return false;
            if (!password.Any(char.IsDigit)) return false;
            return true;
        }
    }
}
