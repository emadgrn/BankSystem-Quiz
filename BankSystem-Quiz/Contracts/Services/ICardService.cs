using BankSystem_Quiz.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem_Quiz.Contracts.Services
{
    public interface ICardService
    {
         bool TransferMoney(string sourceCardNumber, string destinationCardNumber, float amount);
         List<ShowTransactionDto> ShowTransactions(string cardName);
    }
}
