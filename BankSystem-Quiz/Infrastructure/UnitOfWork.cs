using BankSystem_Quiz.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem_Quiz.Infrastructure
{
    public class UnitOfWork(AppDbContext _dbContext)
    {
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
