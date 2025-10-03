using BankSystem_Quiz.Contracts.Repositories;
using BankSystem_Quiz.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem_Quiz.DTO;
using BankSystem_Quiz.Entities;

namespace BankSystem_Quiz.Infrastructure.Repositories
{
    public class TransactionRepository(AppDbContext _dbContext) : ITransactionRepository
    {
        public int Create(Transaction transaction)
        {
            _dbContext.Transactions.Add(transaction);


            return transaction.TransactionId;
        }

        public Transaction GetById(int id)
        {
            var transaction = _dbContext.Transactions.FirstOrDefault(x => x.TransactionId == id);

            if (transaction is null)
                throw new Exception($"Transaction with ID {id} is not found");

            return transaction;
        }

        public List<ShowTransactionDto> GetAllTransactionByCardNumber(string cardNumber)
        {
            return _dbContext.Transactions
                .Where(t=>t.DestinationCardNumber==cardNumber||t.SourceCardNumber==cardNumber)
                .Select(t=>new ShowTransactionDto()
                {
                    TransactionId = t.TransactionId,
                    SourceCardNumber = t.SourceCardNumber,
                    SourceFullName = t.SourceCard.HolderName,
                    DestinationCardNumber = t.DestinationCardNumber,
                    DestinationFullName = t.DestinationCard.HolderName,
                    Amount = t.Amount,
                    IsSuccessful = t.IsSuccessful,
                    TransactionDate = t.TransactionDate
                })
                .ToList();
        }
            public List<Transaction> GetAll()
        {
            return _dbContext.Transactions
                .ToList();
        }

        public void UpdateSuccess(int id,bool isSuccessful)
        {
            var model = GetById(id);

            model.IsSuccessful = isSuccessful;
        }

        public void Delete(int id)
        {
            var model = GetById(id);

            _dbContext.Transactions.Remove(model);
        }
    }

}
