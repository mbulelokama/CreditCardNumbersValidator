using CreditCardNumbersValidator.Models;

namespace CreditCardNumbersValidator.Interfaces
{
    public interface ICreditCardRepository : IRepositoryBase<CreditCard>
    {
        bool CreditCardExist(string creditCardNumber);
    }
}
