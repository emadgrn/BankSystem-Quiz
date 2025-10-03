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

        public void UpdateBalance(Card card)
        {
            var model = GetById(card.CardNumber);

            model.Balance = card.Balance;
        }

        public void UpdateActivation(Card card)
        {
            var model = GetById(card.CardNumber);

            model.IsActive = card.IsActive;
        }

        public void Delete(string cardNumber)
        {
            var model = GetById(cardNumber);

            _dbContext.Cards.Remove(model);
        }
    }
}
