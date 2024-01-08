using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.PayoutModule.Payout.Contracts;

namespace Module.PayoutModule.Payout.Services
{
    public abstract class PaymentService<TModel> : IPayoutService where TModel : IPayoutModel
    {
        

        public Task<dynamic> Payout<T>(T model) where T : IPayoutModel
        {
            return Payout((TModel)(object)model);
        }

        public bool AppliesTo(Type provider)
        {
            return typeof(TModel) == provider;
        }

        protected abstract Task<dynamic> Payout(TModel model);
    }
}
