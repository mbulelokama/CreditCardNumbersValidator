namespace CreditCardNumbersValidator.Models
{
    public class CreditCard
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public CreditCardType CreditCardType { get; set; }

    }
}
