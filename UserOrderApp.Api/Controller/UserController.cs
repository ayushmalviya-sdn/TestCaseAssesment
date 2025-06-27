using Microsoft.AspNetCore.Mvc;
using UserOrderApp.Service;
using UserOrderApp.Interface;
using UserOrderApp.Model;

namespace UserOrderApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IPaymentService _paymentService;
        private readonly IUserRepository _userRepository;

        public UserController(IAuthService authService, IPaymentService paymentService, IUserRepository userRepository)
        {
            _authService = authService;
            _paymentService = paymentService;
            _userRepository = userRepository;
        }

        [HttpPost("validate-password")]
        public IActionResult ValidatePassword([FromBody] string password)
        {
            var isValid = _authService.IsValidPassword(password);
            return Ok(new { isValid });
        }

        [HttpPost("pay")]
        public IActionResult Pay([FromBody] Order order)
        {
            if (order == null)
                return BadRequest("Order cannot be null.");

            try
            {
                _paymentService.ProcessPayment(order);
                return Ok(new
                {
                    order.Id,
                    order.IsPaid,
                    order.PaidAt,
                    order.TransactionId
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
                return NotFound(new { message = $"User with id {id} not found." });

            return Ok(user);
        }
    }
}
