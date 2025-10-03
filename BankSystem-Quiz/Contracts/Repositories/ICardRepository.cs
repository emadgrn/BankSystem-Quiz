using BankSystem_Quiz.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem_Quiz.Contracts.Repositories
{
    public interface ICardRepository
    {
        public string Create(Card card);
        public Card GetById(string cardNumber);
        public List<Card> GetAll();
        public void UpdateBalance(Card card);
        public void UpdateActivation(Card card);
        public void Delete(string cardNumber);
    }
}
