using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem_Quiz.Entities;

namespace BankSystem_Quiz.Infrastructure.Local
{
    public static class LocalStorage
    {
        public static Card? CurrentCard { get; set; }

    }
}
