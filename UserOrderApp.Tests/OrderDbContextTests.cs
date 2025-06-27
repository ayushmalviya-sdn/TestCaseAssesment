using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserOrderApp.Infra;
using UserOrderApp.Model;
using Xunit;

namespace UserOrderApp.Tests
{
    public class OrderDbContextTests
    {
        private DbContextOptions<OrderDbContext> GetInMemoryOptions()
        {
            return new DbContextOptionsBuilder<OrderDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;
        }

        [Fact]
        public void Can_Add_And_Retrieve_Order()
        {
            var options = GetInMemoryOptions();

            using (var context = new OrderDbContext(options))
            {
                var order = new Order
                {
                    Amount = 150,
                    PaymentMethod = "CreditCard",
                    IsPaid = false,
                    CreatedAt = DateTime.UtcNow,
                    TransactionId = new Guid().ToString(),
                };

                context.Orders.Add(order);
                context.SaveChanges();
            }

            using (var context = new OrderDbContext(options))
            {
                var orders = context.Orders.ToList();

                Assert.Single(orders);
                Assert.Equal(150, orders[0].Amount);
                Assert.Equal("CreditCard", orders[0].PaymentMethod);
                Assert.False(orders[0].IsPaid);
            }
        }

        [Fact]
        public void Can_Update_Order_As_Paid()
        {
            var options = GetInMemoryOptions();
            int orderId;

            using (var context = new OrderDbContext(options))
            {
                var order = new Order
                {
                    Amount = 200,
                    PaymentMethod = "UPI",
                    CreatedAt = DateTime.UtcNow,
                    TransactionId = Guid.NewGuid().ToString()

                };
                context.Orders.Add(order);
                context.SaveChanges();
                orderId = order.Id;
            }

            using (var context = new OrderDbContext(options))
            {
                var order = context.Orders.First(o => o.Id == orderId);
                order.IsPaid = true;
                order.PaidAt = DateTime.UtcNow;
                order.TransactionId = Guid.NewGuid().ToString();
                context.SaveChanges();
            }

            using (var context = new OrderDbContext(options))
            {
                var order = context.Orders.First(o => o.Id == orderId);
                Assert.True(order.IsPaid);
                Assert.NotNull(order.PaidAt);
                Assert.False(string.IsNullOrEmpty(order.TransactionId));
            }
        }

        [Fact]
        public void Can_Filter_Unpaid_Orders()
        {
            var options = GetInMemoryOptions();

            using (var context = new OrderDbContext(options))
            {
                context.Orders.AddRange(
                    new Order { Amount = 100, PaymentMethod = "UPI", IsPaid = false , TransactionId = Guid.NewGuid().ToString() },
                    new Order { Amount = 300, PaymentMethod = "NetBanking", IsPaid = true, TransactionId = Guid.NewGuid().ToString() }
                );
                context.SaveChanges();
            }

            using (var context = new OrderDbContext(options))
            {
                var unpaid = context.Orders.Where(o => !o.IsPaid).ToList();
                Assert.Single(unpaid);
                Assert.Equal(100, unpaid[0].Amount);
            }
        }
    }
}
