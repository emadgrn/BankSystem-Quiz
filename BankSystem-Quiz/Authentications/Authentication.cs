using BankSystem_Quiz.Contracts.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem_Quiz.Contracts.Authentications;
using BankSystem_Quiz.Contracts.Repositories;
using BankSystem_Quiz.Extensions;
using BankSystem_Quiz.Infrastructure.Local;

namespace BankSystem_Quiz.Authentications
{
    public class Authentication(ICardRepository _cardRepo, IValidator _validate) : IAuthentication
    {
        public bool Login(string cardNumber, string password)
        {
            if (!_validate.IsValidCardNumber(cardNumber))
            {
                throw new Exception("Card number is invalid!");
            }

            var card = _cardRepo.GetById(cardNumber);

            if (card.Password != password) throw new Exception("Invalid password!");
          

            if (!card.IsActive)
                throw new Exception("This card is not Active! Call the operators.");

            LocalStorage.CurrentCard = card;
            return true;
        }

        public void Logout()
        {
            LocalStorage.CurrentCard = null;
            ConsoleHelper.PrintResult(true, "Logout was successful!");
        }
    }
}
