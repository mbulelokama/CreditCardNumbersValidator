using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardNumbersValidator.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<bool> AddValidCreditCardAsync(T creditCard);
        Task UpdateCreditCardAsync(T creditCardToUpdate);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetCreditCardByIdAsync(string id);
        Task<int> DeleteCreditCardAsync(T creditCard);
        
    }
}
