using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserOrderApp.Model;

namespace UserOrderApp.Service
{
    public interface IPaymentService
    {
        void ProcessPayment(Order order);
    }


}
