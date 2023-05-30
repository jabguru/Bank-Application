using BankApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User(string name, string email, string password) {
            List<User> allUsers = UserService.ReadCSVFile();
            Id = allUsers.Count == 0 ? 1 : allUsers.Count + 1;
            Name = name;
            Email = email;
            Password = password;
        }

        public User()
        {

        }

    }
}
