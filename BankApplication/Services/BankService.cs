using BankApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Services
{
    /// <summary>
    /// This class handles everything relating to services rendered by the bank.
    /// </summary>
    public class BankService
    {
        /// <summary>
        /// The currency being used by the bank.
        /// </summary>
        public static string currency = "$";

        /// <summary>
        /// The bank app starts running from here.
        /// </summary>
        public static void Start()
        {
            ScreenTextService.WelcomeText();
            int value = ScreenTextService.GetIntInput();
            RegisterOrLogin(value);
        }

        /// <summary>
        /// This method handles user's choice to login or register.
        /// </summary>
        /// <param name="value">The value indicates if user is to login or register. 1 - Login, 2 - Register.</param>
        public static void RegisterOrLogin(int value)
        {
            switch (value)
            {
                case 1:
                    // login
                    Login();
                    break;
                case 2:
                    // register
                    Register();
                    break;
                default:
                    // wrong input
                    ScreenTextService.WrongInput();
                    break;
            }
        }

        /// <summary>
        /// This method registers a new user.
        /// </summary>
        private static void Register()
        {
           Dictionary<string, string> userDict =  ScreenTextService.RegisterText();
           User? user = UserService.RegisterUser(userDict);
            if (user != null)
            {
                // create accout for user
                int AccountType = ScreenTextService.CreateAccountText();

                if(AccountType == 1 || AccountType == 2)
                {
                  Account? account =  AccountService.CreateAccount(user.Id, AccountType);
                    if (account != null)
                    {
                        // CREATION SUCCESSFUL

                        // show success text
                        ScreenTextService.RegistrationSuccessful();

                        // take user to login
                        Login();
                        return;
                    }
                }

                ScreenTextService.RegistrationFailed();

            }
            else
            {
                ScreenTextService.RegistrationFailed();
            }
        }


        /// <summary>
        /// This method logs in a registered user.
        /// </summary>
        private static void Login()
        {
            Dictionary<string, string> userDict = ScreenTextService.LoginText();
            User? user = UserService.LoginUser(userDict);

            if(user != null)
            {
                Account? account = AccountService.GetUserAccount(user.Id);
                if (user != null && account != null)
                {
                    Dashboard(user, account);
                }
                else
                {
                    ScreenTextService.LoginFailed();
                }

            }
            else
            {
                ScreenTextService.LoginFailed();
            }


        }

        /// <summary>
        /// This method shows user's dashboard to select actions to perform.
        /// </summary>
        /// <param name="user">Logged in user.</param>
        /// <param name="account">Logged in user's account.</param>
        public static void Dashboard(User user, Account account)
        {
            int value = ScreenTextService.DashboardText(user.Name);
            DashboardAction(value, user, account);
        }

        /// <summary>
        /// This method handles the dashboard action to perfom based on an input value.
        /// </summary>
        /// <param name="value">Input value to decide bank action to perform. 1 - Check balance, 2 - Deposit, 3 - Withdraw, 4 - Transfer, 5 - Exit.</param>
        /// <param name="user">Logged in user.</param>
        /// <param name="account">Logged in user's account.</param>
        public static void DashboardAction(int value, User user, Account account)
        {
            switch (value)
            {
                case 1:
                    // Check Balance
                    CheckBalance(user, account);
                    break;
                case 2:
                    // Deposit
                    Deposit(user, account);
                    break;
                case 3:
                    // withdraw
                    Withdraw(user, account);
                    break;
                case 4:
                    // transfer
                    Transfer(user, account);
                    break;
                case 5:
                    // exit
                    return;
                default:
                    // wrong input
                    ScreenTextService.WrongInput();
                    break;
            }
        }


        /// <summary>
        /// This method checks user's account balance.
        /// </summary>
        /// <param name="user">Logged in user.</param>
        /// <param name="account">Logged in user's account</param>
        public static void CheckBalance(User user, Account account)
        {
            ScreenTextService.CheckBalanceText(account.Balance);
            BackToMainMenu(user, account);
        }

        /// <summary>
        /// This method allows users make deposit to their accounts.
        /// </summary>
        /// <param name="user">Logged in user.</param>
        /// <param name="account">Logged in user's account</param>
        public static void Deposit(User user, Account account)
        {
           double amount = ScreenTextService.DepositText();
           Account? updatedAccount = AccountService.Deposit(amount, account);
            if(updatedAccount != null)
            {
                ScreenTextService.DepositSuccessful(amount);
                ScreenTextService.CheckBalanceText(updatedAccount.Balance, true);
            }
            else
            {
                ScreenTextService.DepositFailed();
            }

            BackToMainMenu(user, account);
        }

        /// <summary>
        ///  This method handles user's withdrawal from account.
        /// </summary>
        /// <param name="user">Logged in user.</param>
        /// <param name="account">Logged in user's account</param>
        public static void Withdraw(User user, Account account)
        {
            double amount = ScreenTextService.WithdrawText();
            Account? updatedAccount = AccountService.Withdraw(amount, account);
            if (updatedAccount != null)
            {
                ScreenTextService.WithdawalSuccessful(amount);
                ScreenTextService.CheckBalanceText(updatedAccount.Balance, true);
            }
            else
            {
                ScreenTextService.WithdrawalFailed();
            }

            BackToMainMenu(user, account);
        }

        /// <summary>
        /// This method handles transfer from logged-in user's account to another.
        /// </summary>
        /// <param name="user">Logged in user.</param>
        /// <param name="account">Logged in user's account</param>
        public static void Transfer(User user, Account account)
        {
            Dictionary<string, dynamic> transferDetails = ScreenTextService.TransferText();
            Account? updatedAccount = AccountService.Transfer(transferDetails, account);
            if (updatedAccount != null)
            {
                ScreenTextService.TransferSuccessful(transferDetails["amount"]);
                ScreenTextService.CheckBalanceText(updatedAccount.Balance, true);
            }
            else
            {
                ScreenTextService.TransferFailed();
            }

            BackToMainMenu(user, account);
        }

        /// <summary>
        /// This method makes it possible for the user to go back to the Main Menu.
        /// </summary>
        /// <param name="user">Logged in user.</param>
        /// <param name="account">Logged in user's account</param>
        public static void BackToMainMenu(User user, Account account)
        {
         int value =   ScreenTextService.BackToMainMenuText();
            if(value == 1)
            {
                Dashboard(user, account);
            }
            else
            {
                return;
            }
        }
    }
}
