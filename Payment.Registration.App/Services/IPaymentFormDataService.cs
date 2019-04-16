using System.Threading.Tasks;
using Payment.Registration.Domain.Models;

namespace Payment.Registration.App.Services
{
    public interface IPaymentFormDataService : IDataService<PaymentForm>
    {
        Task<int> GetNewPaymentFormNumber();
    }
}