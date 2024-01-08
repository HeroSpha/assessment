using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.PayoutModule.Payout.Contracts;

namespace Module.PayoutModule.Payout.Models
{
    public class StandardBankInstant : IPayoutModel
    {
        public string PhoneNumber { get; set; }
    }
}
