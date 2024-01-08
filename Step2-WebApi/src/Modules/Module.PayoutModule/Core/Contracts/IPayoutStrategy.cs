using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.PayoutModule.Payout.Contracts;

namespace Module.PayoutModule.Core.Contracts
{
    public interface IPayoutStrategy
    {
        Task<dynamic> MakePayment<T>(T model) where T : IPayoutModel;
    }
}
