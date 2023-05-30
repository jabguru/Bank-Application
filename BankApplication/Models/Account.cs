using BankApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum AccountType
{
    Savings = 1,
    Current = 2,
}

namespace BankApplication.Models
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public int UserId { get; set; }
        public int AccountType { get; set; }
        public double Balance { get; set; }

        public Account(int userId, int accountType)
        {
            List<Account> allAccounts = AccountService.ReadCSVFile();
            AccountNumber = allAccounts.Count == 0 ? 1808050001 : allAccounts.Count + 1808050001;
            UserId = userId;
            AccountType = accountType;
            Balance = 0;
        }

        public Account()
        {

        }
    }
}
