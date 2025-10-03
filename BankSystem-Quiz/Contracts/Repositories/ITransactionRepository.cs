using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem_Quiz.DTO;
using BankSystem_Quiz.Entities;

namespace BankSystem_Quiz.Contracts.Repositories
{
    public interface ITransactionRepository
    {
        public int Create(Transaction transaction);
        public Transaction GetById(int id);
        public List<Transaction> GetAll();
        public List<ShowTransactionDto> GetAllTransactionByCardNumber(string cardNumber);
        public void UpdateSuccess(int id, bool isSuccessful);
        public void Delete(int id);
    }
}
