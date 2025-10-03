using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem_Quiz.Contracts.Validators;

namespace BankSystem_Quiz.Validators
{
    public  class Validator:IValidator
    {
        public bool IsValidCardNumber(string cardNumber)
        {
            return !string.IsNullOrWhiteSpace(cardNumber) && cardNumber.Length == 16 && cardNumber.All(char.IsDigit);
        }
        public bool IsValidAmount(float amount)
        {
            return amount > 0;
        }
    }
}
