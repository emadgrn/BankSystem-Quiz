using BankSystem_Quiz.Contracts.Repositories;
using BankSystem_Quiz.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public List<Transaction> GetAll()
        {
            return _dbContext.Transactions
                .Include(b => b.Category)
                .ToList();
        }

        public void UpdateSuccess(Transaction transaction)
        {
            var model = GetById(transaction.TransactionId);

            model.isSuccessful = transaction.isSuccessful;
        }

        public void Delete(int id)
        {
            var model = GetById(id);

            _dbContext.Transactions.Remove(model);
        }
    }

}
