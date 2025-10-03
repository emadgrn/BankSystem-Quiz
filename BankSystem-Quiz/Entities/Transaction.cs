using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem_Quiz.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        [ForeignKey("SourceCard")]
        public string SourceCardNumber { get; set; }
        public Card SourceCard { get; set; }

        [ForeignKey("DestinationCard")]
        public string DestinationCardNumber { get; set; }
        public Card DestinationCard { get; set; }
        public float Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool isSuccessful { get; set; }


    }
}
