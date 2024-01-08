using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.PayoutModule.Core.Contracts;
using Module.PayoutModule.Payout.Contracts;
using Module.PayoutModule.Payout.Services;

namespace Module.PayoutModule.Core.Services
{
    public class PayoutStrategy : IPayoutStrategy
    {
        private readonly IEnumerable<IPayoutService> _payoutServices;

        public PayoutStrategy(IEnumerable<IPayoutService> payoutServices)
        {
            _payoutServices = payoutServices ?? throw new ArgumentNullException(nameof(payoutServices));
        }
        public async Task<dynamic> MakePayment<T>(T model) where T : IPayoutModel
        {
            return await GetPaymentService(model).Payout(model);
        }

        private IPayoutService GetPaymentService<T>(T model) where T : IPayoutModel
        {
            var result = _payoutServices.FirstOrDefault(p => p.AppliesTo(model.GetType()));
            if (result == null)
            {
                throw new InvalidOperationException(
                    $"Payment service for {model.GetType()} not registered.");
            }

            return result;
        }
    }
}
