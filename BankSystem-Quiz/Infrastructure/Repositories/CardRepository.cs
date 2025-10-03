using BankSystem_Quiz.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem_Quiz.Contracts.Repositories;
using BankSystem_Quiz.Entities;

namespace BankSystem_Quiz.Infrastructure.Repositories
{
    public class CardRepository(AppDbContext _dbContext) : ICardRepository
    {
        public string Create(Card card)
        {
            _dbContext.Cards.Add(card);

            return card.CardNumber;
        }

        public bool Exist(string cardNumber)
        {
            return _dbContext.Cards.Any(x => x.CardNumber == cardNumber);
        }
        public Card GetById(string cardNumber)
        {
            var card = _dbContext.Cards.FirstOrDefault(x => x.CardNumber == cardNumber);

            if (card is null)
                throw new Exception($"Card with number {cardNumber} is not found");

            return card;
        }

        public List<Card> GetAll()
        {
            return _dbContext.Cards
                .ToList();
        }

        public void UpdateBalance(string cardNumber,float balance)
        {
            var model = GetById(cardNumber);

            model.Balance = balance;
        }

        public void UpdateActivation(string cardNumber, bool isActive)
        {
            var model = GetById(cardNumber);

            model.IsActive =isActive;
        }

        public void Delete(string cardNumber)
        {
            var model = GetById(cardNumber);

            _dbContext.Cards.Remove(model);
        }
    }
}
