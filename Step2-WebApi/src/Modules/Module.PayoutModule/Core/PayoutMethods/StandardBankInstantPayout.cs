using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.PayoutModule.Payout.Contracts;
using Module.PayoutModule.Payout.Models;
using Module.PayoutModule.Payout.Services;

namespace Module.PayoutModule.Payout.PayoutMethods
{
    public class StandardBankInstantPayout : PaymentService<StandardBankInstant>
    {
        protected override Task<dynamic> Payout(StandardBankInstant model)
        {
            throw new NotImplementedException();
        }
    }
}
