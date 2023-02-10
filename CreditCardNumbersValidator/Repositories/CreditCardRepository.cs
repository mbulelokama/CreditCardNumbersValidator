using CreditCardNumbersValidator.Data;
using CreditCardNumbersValidator.Interfaces;
using CreditCardNumbersValidator.Models;
using System.Linq;

namespace CreditCardNumbersValidator.Repositories
{
    public class CreditCardRepository : RepositoryBase<CreditCard>,ICreditCardRepository
    {
        public CreditCardRepository(CreditCardNumbersValidatorContext DbContext) : base(DbContext)
        {

        }

        public bool CreditCardExist(string creditCardNumber)
        {
            return _context.CreditCard.Any(c => c.CardNumber == creditCardNumber);
        }
    }
}
