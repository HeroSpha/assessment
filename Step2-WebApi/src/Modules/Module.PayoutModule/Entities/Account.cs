using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PayoutModule.Entities
{
    public class Account
    {
        public decimal Balance { get; set; }
        public decimal Threshold { get; set; }
    }
}
