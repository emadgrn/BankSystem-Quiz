using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem_Quiz.Entities
{
    public class Card
    {
        [Key]
        public string CardNumber { get; set; }
        public string HolderName { get; set; }
        public string Password { get; set; }
        public float Balance { get; set; }
        public bool IsActive { get; set; }
        public List<Transaction> SentTransactions { get; set; }
        public List<Transaction> ReceivedTransactions { get; set; }
    }
}
