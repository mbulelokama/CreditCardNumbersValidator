using CreditCardNumbersValidator.Data;
using CreditCardNumbersValidator.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CreditCardNumbersValidator.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly CreditCardNumbersValidatorContext _context;

        public RepositoryBase(CreditCardNumbersValidatorContext context)
        {
            _context = context;
          
        }

        public async Task<bool> AddValidCreditCardAsync(T creditCard)
        {
            await _context.Set<T>().AddAsync(creditCard);
            var added = await _context.SaveChangesAsync();
            return added > 0;
        }

        public async Task<int> DeleteCreditCardAsync(T creditCard)
        {
            _context.Set<T>().Remove(creditCard);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetCreditCardByIdAsync(string id)
            => await _context.Set<T>().FindAsync(id);

        public Task UpdateCreditCardAsync(T creditCardToUpdate)
        {
            _context.Entry(creditCardToUpdate).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

        
    }
}
