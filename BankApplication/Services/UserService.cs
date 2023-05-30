using BankApplication.Mappers;
using BankApplication.Models;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace BankApplication.Services
{
    /// <summary>
    /// This class handles everything relating to a user.
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// Location for the CSV file (database) where users are stored.
        /// </summary>
        private static string _location = @"C:\Users\Julius Alibrown\Desktop\class\BankApplication\db\User.csv";


        /// <summary>
        /// Reads existing CSV file where users are stored.
        /// </summary>
        /// <returns>Returns the list of all users.</returns>
        /// <exception cref="Exception"></exception>
        public static List<User> ReadCSVFile()
        {
            try
            {
                using (var reader = new StreamReader(_location, Encoding.Default))
                using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
                {
                    csv.Context.RegisterClassMap<UserMap>();
                    var records = csv.GetRecords<User>().ToList();
                    return records;
                }
        }
            catch (Exception e)
            {
                throw new Exception(e.Message);
    }
}

        /// <summary>
        /// Adds a user to the CSV file.
        /// </summary>
        /// <param name="user">User to be added to the database.</param>
        public static void WriteCSVFile(User user)
        {
            List<User> allUsers = ReadCSVFile();

            using (StreamWriter sw = new StreamWriter(_location, true, new UTF8Encoding(true)))
            using (CsvWriter cw = new CsvWriter(sw, CultureInfo.CurrentCulture))
            {
                if(allUsers.Count == 0)
                {
                    cw.WriteHeader<User>();
                    cw.NextRecord();
                }
                cw.WriteRecord<User>(user);
                cw.NextRecord();
            }
        }

        /// <summary>
        /// The method that registers a user and updates the database.
        /// </summary>
        /// <param name="userDict"></param>
        /// <returns>Returns the registered user object.</returns>
        public static User? RegisterUser(Dictionary<string, string> userDict)
        {
            try
            {
                string name = userDict["name"];
                string email = userDict["email"];
                string password = userDict["password"];

                List<User> allUsers = ReadCSVFile();
                
                if ((allUsers.Find(x => x.Email == email)) != null)
                {
                    return null;
                }


                User user = new User(name, email, password);
                WriteCSVFile(user);
                return user;
            }
            catch (Exception)
            {
                return null;
            }
           
        }

        /// <summary>
        /// This method logs in an existing user.
        /// </summary>
        /// <param name="userDict"></param>
        /// <returns>Logged in user object.</returns>
        public static User? LoginUser(Dictionary<string, string> userDict)
        {
            try
            {
                string email = userDict["email"];
                string password = userDict["password"];

                List<User> allUsers = ReadCSVFile();
                User? user = allUsers.Find(x => x.Email == email && x.Password == password);
                if (user != null)
                {
                    return user;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// This method gets a user from the DB with a userId
        /// </summary>
        /// <param name="userId">The Id for the user to be retrieved.</param>
        /// <returns>User object for the retreived user.</returns>
        public static User? GetUser(int userId)
        {
            try
            {
                List<User> allUsers = ReadCSVFile();
                User? user = allUsers.Find(x => x.Id == userId);

                if (user != null)
                {
                    return user;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
