using BankApplication.Models;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Mappers
{
    public sealed class AccountMap : ClassMap<Account>
    {
        public AccountMap()
        {
            Map(x => x.AccountNumber).Name("AccountNumber");
            Map(x => x.AccountType).Name("AccountType");
            Map(x => x.UserId).Name("UserId");
            Map(x => x.Balance).Name("Balance");
        }
    }
}
