using BankApplication.Models;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Mappers
{
    public sealed class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Map(x => x.Id).Name("Id");
            Map(x => x.Email).Name("Email");
            Map(x => x.Password).Name("Password");
            Map(x => x.Name).Name("Name");
        }
    }
}
