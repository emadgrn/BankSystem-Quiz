using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem_Quiz.Contracts.Repositories;
using BankSystem_Quiz.Contracts.Services;
using BankSystem_Quiz.DTO;
using BankSystem_Quiz.Entities;
using BankSystem_Quiz.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BankSystem_Quiz.Services
{
    public class CardService(ICardRepository _cardRepo, ITransactionRepository _transactionRepo, UnitOfWork _unitOfWork) : ICardService
    {
        public bool TransferMoney(string sourceCardNumber, string destinationCardNumber, float amount)
        {
            var sourceCard = _cardRepo.GetById(sourceCardNumber);
            var destinationCard = _cardRepo.GetById(destinationCardNumber);

            if (sourceCard == null || destinationCard == null) return false;
            if (sourceCard.Balance < amount) throw new Exception("There isn't enough money!");

            _cardRepo.UpdateBalance(sourceCard.CardNumber,(sourceCard.Balance-amount));
            _cardRepo.UpdateBalance(destinationCard.CardNumber, (destinationCard.Balance + amount));

            var transaction = new Transaction
            {
                SourceCardNumber = sourceCardNumber,
                DestinationCardNumber = destinationCardNumber,
                Amount = amount,
                TransactionDate = DateTime.Now,
                IsSuccessful = true
            };
            _transactionRepo.Create(transaction);

            _unitOfWork.Save();

            return true;
        }
        public List<ShowTransactionDto> ShowTransactions(string cardName)
        {
            return _transactionRepo.GetAllTransactionByCardNumber(cardName) ??
                   throw new Exception("There isn't any transaction!");
        }
    }
}
