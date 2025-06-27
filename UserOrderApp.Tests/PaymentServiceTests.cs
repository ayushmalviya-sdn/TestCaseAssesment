using System;
using UserOrderApp.Model;
using UserOrderApp.Service;
using Xunit;

namespace UserOrderApp.Tests
{
    public class PaymentServiceTests
    {
        private readonly PaymentService _service = new PaymentService();

        [Fact]
        public void ProcessPayment_ValidOrder_ShouldSetPaidProperties()
        {
            var order = new Order
            {
                Id = 1,
                Amount = 100,
                PaymentMethod = "CreditCard",
                CreatedAt = DateTime.UtcNow.AddHours(-1),
                IsPaid = false
            };

            _service.ProcessPayment(order);

            Assert.True(order.IsPaid);
            Assert.NotNull(order.PaidAt);
            Assert.False(string.IsNullOrEmpty(order.TransactionId));
        }

        [Fact]
        public void ProcessPayment_NullOrder_ShouldThrow()
        {
            Assert.Throws<InvalidOperationException>(() => _service.ProcessPayment(null));
        }

        [Fact]
        public void ProcessPayment_AlreadyPaid_ShouldThrow()
        {
            var order = new Order
            {
                IsPaid = true,
                Amount = 100,
                PaymentMethod = "UPI",
                CreatedAt = DateTime.UtcNow
            };

            Assert.Throws<InvalidOperationException>(() => _service.ProcessPayment(order));
        }

        [Fact]
        public void ProcessPayment_AmountZeroOrNegative_ShouldThrow()
        {
            var order = new Order
            {
                Amount = 0,
                IsPaid = false,
                PaymentMethod = "UPI",
                CreatedAt = DateTime.UtcNow
            };

            Assert.Throws<InvalidOperationException>(() => _service.ProcessPayment(order));
        }

        [Fact]
        public void ProcessPayment_EmptyPaymentMethod_ShouldThrow()
        {
            var order = new Order
            {
                Amount = 100,
                IsPaid = false,
                PaymentMethod = "",
                CreatedAt = DateTime.UtcNow
            };

            Assert.Throws<InvalidOperationException>(() => _service.ProcessPayment(order));
        }

        [Fact]
        public void ProcessPayment_UnsupportedPaymentMethod_ShouldThrow()
        {
            var order = new Order
            {
                Amount = 100,
                IsPaid = false,
                PaymentMethod = "bitcoin",
                CreatedAt = DateTime.UtcNow
            };

            Assert.Throws<InvalidOperationException>(() => _service.ProcessPayment(order));
        }

        [Fact]
        public void ProcessPayment_ExpiredOrder_ShouldThrow()
        {
            var order = new Order
            {
                Amount = 100,
                IsPaid = false,
                PaymentMethod = "UPI",
                CreatedAt = DateTime.UtcNow.AddHours(-25)
            };

            Assert.Throws<InvalidOperationException>(() => _service.ProcessPayment(order));
        }
    }
}
