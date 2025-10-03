using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem_Quiz.Contracts.Validators
{
    public interface IValidator
    {
         bool IsValidCardNumber(string cardNumber);
         bool IsValidAmount(float amount);
        
    }
}
