using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem_Quiz.Contracts.Authentications
{
    public interface IAuthentication
    {
        public bool Login(string cardNumber, string password);
        public void Logout();
    }
}
