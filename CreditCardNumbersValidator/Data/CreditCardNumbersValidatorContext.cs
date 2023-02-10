using Microsoft.EntityFrameworkCore;
using CreditCardNumbersValidator.Models;

namespace CreditCardNumbersValidator.Data
{
    public class CreditCardNumbersValidatorContext : DbContext
    {
        public CreditCardNumbersValidatorContext (DbContextOptions<CreditCardNumbersValidatorContext> options)
            : base(options)
        {
        }

        public DbSet<CreditCard> CreditCard { get; set; }
    }
}
