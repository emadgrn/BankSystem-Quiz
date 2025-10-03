using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem_Quiz.DTO
{
    public class ShowTransactionDto
    {
        public int TransactionId { get; set; }
        public string SourceCardNumber { get; set; }
        public string SourceFullName { get; set; }
        public string DestinationCardNumber { get; set; }
        public string DestinationFullName { get; set; }
        public float Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
