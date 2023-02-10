using CreditCardNumbersValidator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreditCardNumbersValidator.Interfaces
{
    public interface ICreditCardNumberService
    {
        Task<string> AddValidCreditCardAsync(CreditCard creditCard);
        Task<IEnumerable<CreditCard>> GetAll();
    }
}
