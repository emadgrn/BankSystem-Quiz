using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem_Quiz.Entities;

namespace BankSystem_Quiz.Contracts.Repositories
{
    public interface ITransactionRepository
    {
        public int Create(Transaction transaction);
        public Transaction GetById(int id);
        public List<Transaction> GetAll();
        public void UpdateSuccess(Transaction transaction);
        public void Delete(int id);
    }
}
