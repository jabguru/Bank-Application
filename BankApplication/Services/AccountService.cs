using BankApplication.Mappers;
using BankApplication.Models;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Services
{
    /// <summary>
    /// This class handles things related to a user's account.
    /// </summary>
    public class AccountService
    {
        /// <summary>
        /// Location for the CSV file (database) where accounts are stored.
        /// </summary>
        private static string _location = @"C:\Users\Julius Alibrown\Desktop\class\Bank Application\BankApplication\db\Account.csv";

        /// <summary>
        /// Reads existing CSV file where accounts are stored.
        /// </summary>
        /// <returns>Returns the list of all accounts.</returns>
        /// <exception cref="Exception"></exception>
        public static List<Account> ReadCSVFile()
        {
            try
            {
                using (var reader = new StreamReader(_location, Encoding.Default))
                using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
                {
                    csv.Context.RegisterClassMap<AccountMap>();
                    var records = csv.GetRecords<Account>().ToList();
                    return records;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Adds an account to the CSV file.
        /// </summary>
        /// <param name="account">Account to be added to the database.</param>
        public static void WriteCSVFile(Account account)
        {
            List<Account> allAccounts = ReadCSVFile();

            using (StreamWriter sw = new StreamWriter(_location, true, new UTF8Encoding(true)))
            using (CsvWriter cw = new CsvWriter(sw, CultureInfo.CurrentCulture))
            {
                if (allAccounts.Count == 0)
                {
                    cw.WriteHeader<Account>();
                    cw.NextRecord();
                }
                cw.WriteRecord<Account>(account);
                cw.NextRecord();
            }
        }

        /// <summary>
        /// This method overwrites the DB with a list of accounts
        /// </summary>
        /// <param name="accounts">List of accounts to be added to the DB.</param>
        public static void OverwriteCSVFile( List<Account> accounts)
        {
            using (StreamWriter sw = new StreamWriter(_location, false, new UTF8Encoding(true)))
            using (CsvWriter cw = new CsvWriter(sw, CultureInfo.CurrentCulture))
            {
                cw.WriteHeader<Account>();
                cw.NextRecord();
                foreach (Account account in accounts)
                {
                    cw.WriteRecord<Account>(account);
                    cw.NextRecord();
                }
            }
        }

        /// <summary>
        /// This method creates a new account with a user ID and account type.
        /// </summary>
        /// <param name="userId">ID of the owning user.</param>
        /// <param name="accountType">Value for the type of account to be created.</param>
        /// <returns>Returns the created account object or null if an error occurs.</returns>
        public static Account? CreateAccount(int userId, int accountType)
        {
            try
            {
                List<Account> allAccounts = ReadCSVFile();

                if ((allAccounts.Find(x => x.UserId == userId)) != null)
                {
                    return null;
                }


                Account account = new Account(userId, accountType);
                WriteCSVFile(account);
                return account;
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// This method gets a user account with the user's ID.
        /// </summary>
        /// <param name="userId">ID of owning user.</param>
        /// <returns>Returns user account or null if an error occurs.</returns>
        public static Account? GetUserAccount(int userId)
        {
            try
            {
                List<Account> allAccounts = ReadCSVFile();
                Account? account = allAccounts.Find(x => x.UserId == userId);

                if (account != null)
                {
                    return account;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// This method gets a user account with the user's account number.
        /// </summary>
        /// <param name="accountNumber">User's Account number.</param>
        /// <returns>Returns user account or null if an error occurs.</returns>
        public static Account? GetUserAccountFromAccountNumber(int accountNumber)
        {
            try
            {
                List<Account> allAccounts = ReadCSVFile();
                Account? account = allAccounts.Find(x => x.AccountNumber == accountNumber);

                if (account != null)
                {
                    return account;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }

        /// <summary>
        /// This method deposits an amount to the user's account.
        /// </summary>
        /// <param name="amount">Amount to be deposited.</param>
        /// <param name="account">User's account.</param>
        /// <returns></returns>
        public static Account? Deposit(double amount, Account account)
        {
            try
            {
                if (amount > 0)
                {
                    double newBalance = account.Balance + amount;
                    account.Balance = newBalance;
                    bool updated = UpdateAccount(account);
                    if (updated)
                    {
                        return account;
                    }

                    return null;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// This method withdraws an ammount from the user's account.
        /// </summary>
        /// <param name="amount">Amount to be withdrawn.</param>
        /// <param name="account">User's account.</param>
        /// <returns></returns>
        public static Account? Withdraw(double amount, Account account)
        {
            try
            {
                if (account.Balance >= amount && amount > 0)
                {
                    double newBalance = account.Balance - amount;
                    account.Balance = newBalance;
                    bool updated = UpdateAccount(account);
                    if (updated)
                    {
                        return account;
                    }

                    return null;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// This method transfers an amount from logged-in user's account to another account.
        /// </summary>
        /// <param name="transferDetails">A dictionary that contains the amount to be transferred and the reciever's account number.</param>
        /// <param name="account"></param>
        /// <returns>Returns user account or null if an error occurs.</returns>
        public static Account? Transfer(Dictionary<string, dynamic> transferDetails, Account account)
        {
            try
            {
                double amount = transferDetails["amount"];
                int recieverAccountNumber = transferDetails["accountNumber"];

                if(recieverAccountNumber == account.AccountNumber)
                {
                    // you cannot transfer to your own account
                    return null;
                }

                Account? recieverAccount = GetUserAccountFromAccountNumber(recieverAccountNumber);

                if (recieverAccount != null)
                {
                    User? recieverAccountUser = UserService.GetUser(recieverAccount.UserId);
                    int value = ScreenTextService.TransferConfirmation(recieverAccountUser.Name, amount);

                    if(value == 1)
                    {
                        if (account.Balance >= amount && amount > 0)
                        {
                            Account? senderAccount = Withdraw(amount, account);

                            if (senderAccount != null)
                            {
                                Account? updatedRecieverAccount = Deposit(amount, recieverAccount!);
                                if (updatedRecieverAccount != null)
                                {
                                    return senderAccount;
                                }
                                else
                                {
                                    // sender was debited but reciever wasn't paid,
                                    // REFUND
                                    Deposit(amount, senderAccount);
                                }
                            }
                        }
                    }
                }
                else
                {
                    ScreenTextService.TransferAccountNotFoundError(recieverAccountNumber);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// This method updates a user account with new account details.
        /// </summary>
        /// <param name="account">Account object with udpated details</param>
        /// <returns>Returns true if successful and false if not.</returns>
        public static bool UpdateAccount(Account account)
        {
            try
            {
                List<Account> allAccounts = ReadCSVFile();
                int oldAccountIndex = allAccounts.FindIndex(x => x.UserId == account.UserId);
                allAccounts[oldAccountIndex] = account;
                OverwriteCSVFile(allAccounts);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }
    }

}
