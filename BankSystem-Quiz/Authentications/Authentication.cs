using BankSystem_Quiz.Contracts.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem_Quiz.Contracts.Authentications;
using BankSystem_Quiz.Contracts.Repositories;
using BankSystem_Quiz.Extensions;
using BankSystem_Quiz.Infrastructure;
using BankSystem_Quiz.Infrastructure.Local;

namespace BankSystem_Quiz.Authentications
{
    public class Authentication(ICardRepository _cardRepo, IValidator _validate,UnitOfWork _unitOfWork) : IAuthentication
    {
        // public bool Login(string cardNumber, string password)
        // {
        //     if (!_validate.IsValidCardNumber(cardNumber))
        //     {
        //         throw new Exception("Card number is invalid!");
        //     }
        //
        //     var card = _cardRepo.GetById(cardNumber);
        //
        //     if (card.Password != password) throw new Exception("Invalid password!");
        //   
        //
        //     if (!card.IsActive)
        //         throw new Exception("This card is not Active! Call the operators.");
        //
        //     LocalStorage.CurrentCard = card;
        //     return true;
        // }


        public bool Login(string cardNumber, string password)
        {
            if (!_validate.IsValidCardNumber(cardNumber))
                throw new Exception("Card number is invalid!");

            var card = _cardRepo.GetById(cardNumber);

            if (!card.IsActive)
                throw new Exception("This card is not Active! Call the operators.");

            if (card.Password != password)
            {
                card.FailedLoginAttempts++;
                

                if (card.FailedLoginAttempts >= 3)
                {
                    card.IsActive = false;
                    _unitOfWork.Save();
                    throw new Exception("Card blocked due to 3 failed login attempts.");
                }
                else
                {
                    _unitOfWork.Save();
                    throw new Exception($"Invalid password! Attempt {card.FailedLoginAttempts}/3.");
                }
            }

            card.FailedLoginAttempts = 0;
            _unitOfWork.Save();

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
