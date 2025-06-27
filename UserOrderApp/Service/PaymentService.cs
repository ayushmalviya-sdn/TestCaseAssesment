using System;
using UserOrderApp.Model;

namespace UserOrderApp.Service
{
    public class PaymentService : IPaymentService
    {
        public void ProcessPayment(Order order)
        {
            if (order == null)
                throw new InvalidOperationException("Order is null.");

            if (order.IsPaid)
                throw new InvalidOperationException("Order is already paid.");

            if (order.Amount <= 0)
                throw new InvalidOperationException("Payment amount must be greater than zero.");

            if (string.IsNullOrWhiteSpace(order.PaymentMethod))
                throw new InvalidOperationException("Payment method is required.");

            if (!IsSupportedPaymentMethod(order.PaymentMethod))
                throw new InvalidOperationException($"Unsupported payment method: {order.PaymentMethod}");

            if (order.CreatedAt.AddHours(24) < DateTime.UtcNow)
                throw new InvalidOperationException("Order has expired and cannot be paid.");

            // Simulate payment processing
            order.IsPaid = true;
            order.PaidAt = DateTime.UtcNow;
            order.TransactionId = Guid.NewGuid().ToString();
        }

        private bool IsSupportedPaymentMethod(string method)
        {
            return method.ToLower() switch
            {
                "creditcard" => true,
                "debitcard" => true,
                "upi" => true,
                "netbanking" => true,
                _ => false
            };
        }
    }
}
