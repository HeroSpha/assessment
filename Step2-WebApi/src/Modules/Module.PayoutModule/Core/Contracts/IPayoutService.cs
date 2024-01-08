using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PayoutModule.Payout.Contracts
{
    public interface IPayoutService
    {
        Task<dynamic> Payout<T>(T model) where T : IPayoutModel;
        bool AppliesTo(Type provider);
    }
}
