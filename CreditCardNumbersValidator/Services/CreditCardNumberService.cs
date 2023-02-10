using CreditCardNumbersValidator.Interfaces;
using CreditCardNumbersValidator.Models;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CreditCardNumbersValidator.Services
{
    public class CreditCardNumberService : ICreditCardNumberService
    {
        private readonly ICreditCardRepository _creditCardNumberRepository;

        public CreditCardNumberService(ICreditCardRepository creditCardNumberRepository)
        {
            _creditCardNumberRepository = creditCardNumberRepository;
        }

        public async Task<string> AddValidCreditCardAsync(CreditCard creditCard)
        {
            var exist = _creditCardNumberRepository.CreditCardExist(creditCard.CardNumber);

            if (!exist)
            {
                var creditCardType = FindType(creditCard.CardNumber);

                creditCard = new CreditCard
                {
                    Id = creditCard.Id,
                    CardNumber = creditCard.CardNumber,
                    CreditCardType = creditCardType
                };


                if (creditCard.CreditCardType != CreditCardType.UnknownCard)
                {
                    var valid = ValidateCardNumber(creditCard.CardNumber);

                    if (valid)
                    {
                        var added = await _creditCardNumberRepository.AddValidCreditCardAsync(creditCard);
                        if (added)
                        {
                            return "Successfully saved into the DB!, this " + creditCard.CardNumber + " is a valid and allowed " + creditCard.CreditCardType + " credit card.";
                        }
                        else
                        {
                            return "Failed to save the credit card number into the DB! :(";
                        }
                    }
                    else
                    {
                        return "Invalid Credit card number!!!! Please try a different credit card number.";
                    }
                }
                else
                {
                    return "Uknown credit card number. Please check with your credit card provider and try again";
                }
            }
            else
            {
                return "We cannot capture the same valid card twice. This credit card number already exist in our records.";
            }


        }
        public async Task<IEnumerable<CreditCard>> GetAll()
        {
            var creditCardList = await _creditCardNumberRepository.GetAll();
            return creditCardList;
        }

        public static CreditCardType FindType(string cardNumber)
        {

            if (Regex.Match(cardNumber.ToString(), @"^4[0-9]{12}(?:[0-9]{3})?$").Success)
            {
                return CreditCardType.VISA;
            }

            if (Regex.Match(cardNumber.ToString(), @"^3[47][0-9]{13}$").Success)
            {
                return CreditCardType.AMEX;
            }

            if (Regex.Match(cardNumber.ToString(), @"^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$").Success)
            {
                return CreditCardType.MasterCard;
            }

            if (Regex.Match(cardNumber.ToString(), @"^6(?:011|5[0-9]{2})[0-9]{12}$").Success)
            {
                return CreditCardType.Discover;
            }

            return CreditCardType.UnknownCard;
        }
        public bool ValidateCardNumber(string cardNumber)
        {
            //convert cardNumber to Int array
            int[] cardInt = new int[cardNumber.Length];

            for (int i = 0; i < cardNumber.Length; i++)
            {
                cardInt[i] = (int)(cardNumber[i] - '0');
            }

            //Starting from the right, double each other digit, if greater than 9, mod 10 and + 1 to remaider
            for (int i = cardInt.Length - 2; i >=0 ; i -= 2)
            {
                int tempValue = cardInt[i];
                tempValue *= 2;
                if (tempValue > 9)
                {
                    tempValue = tempValue % 10 + 1;
                }
                cardInt[i] = tempValue;
            }

            //Add up all digits
            int total = 0;
            for (int i = 0; i < cardInt.Length; i++)
            {
                total += cardInt[i];
            }

            //if number is multiple of 10, it is valid
            if (total % 10 ==0)
            {
                return true;
            }

            return false;
        }
    }
}
