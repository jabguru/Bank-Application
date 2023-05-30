using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Services
{
    /// <summary>
    /// This class handles everything relating to displaying and getting inputs on the console.
    /// </summary>
    public class ScreenTextService
    {
        /// <summary>
        /// A string with currency used by input.
        /// </summary>
        public static string amountText = $"Amount: {BankService.currency}";

        /// <summary>
        /// This method handles getting integer inputs.
        /// </summary>
        /// <param name="text">Text to display in the console.</param>
        /// <param name="clear">Clears console if true.</param>
        /// <returns></returns>
        public static int GetIntInput(string text = "Input: ", bool clear = true)
        {
            Console.Write(text);
            string inputString = Console.ReadLine();
            int input = int.Parse(inputString);

            if (clear)
            {
                Console.Clear();
            }

            return input;
        }

        /// <summary>
        /// This method handles getting double inputs
        /// </summary>
        /// <param name="text">Text to display in the console.</param>
        /// <param name="clear">Clears console if true.</param>
        /// <returns></returns>
        public static double GetDoubleInput(string text = "Input: ", bool clear = true)
        {
            Console.Write(text);
            string inputString = Console.ReadLine();
            double input = double.Parse(inputString);

            if (clear)
            {
                Console.Clear();
            }

            return input;
        }

        /// <summary>
        /// This method handles getting string inputs
        /// </summary>
        /// <param name="text">Text to display in the console.</param>
        /// <returns></returns>
        public static string GetStringInput(string text = "Input: ")
        {
            Console.Write(text);
            string inputString = Console.ReadLine();
            return inputString;
        }

        /// <summary>
        /// Displays Welcome Text. And perfomable actions (Login & Register).
        /// </summary>
        public static void WelcomeText()
        {
            Console.WriteLine("Welcome To CSC Bank!\n");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine();
        }

        /// <summary>
        /// Displays Login Texts and recieves inputs.
        /// </summary>
        /// <returns>Returns a dictionary containing login details (email & password)</returns>
        public static Dictionary<string, string> LoginText()
        {
            Console.WriteLine("Login To Your Account\n");
            Console.WriteLine();
            string email = GetStringInput("Email: ");
            string password = GetStringInput("Password: ");

            Dictionary<string, string> dict = new Dictionary<string, string>
            {
                {"email", email },
                {"password", password },

            };

            return dict;
        }

        /// <summary>
        /// Displays registration screen texts and recieves inputs.
        /// </summary>
        /// <returns>Returns a dictionary of the registration details (full name, email and password).</returns>
        public static Dictionary<string, string> RegisterText()
        {
            Console.WriteLine("Register An Account\n");
            Console.WriteLine();
            string fullName = GetStringInput("Full Name: ");
            string email = GetStringInput("Email: ");
            string password = GetStringInput("Password: ");

            Dictionary<string, string> dict = new Dictionary<string, string>
            {
                {"name", fullName },
                {"email", email },
                {"password", password },

            };

            return dict;
        }

        /// <summary>
        /// This method displays texts shown to create account.
        /// </summary>
        /// <returns>Returns the value of the account type to be created.</returns>
        public static int CreateAccountText()
        {
            Console.Clear();
            Console.WriteLine("Select Account Type\n");
            Console.WriteLine("1. Savings");
            Console.WriteLine("2. Current");
            Console.WriteLine();

            int input = GetIntInput();

            return input;
        }

        /// <summary>
        /// This method displays dashboard texts and recieves input of the action to be performed.
        /// </summary>
        /// <param name="name">Logged-in user's name.</param>
        /// <returns>Returns the value of the banking action to be performed.</returns>
        public static int DashboardText(string name)
        {
            Console.Clear();
            Console.WriteLine($"Welcome {name}! \n\n");

            Console.WriteLine("What do you want to do?");

            Console.WriteLine("1. Check balance");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Transfer");
            Console.WriteLine("5. Exit");
            Console.WriteLine();

            int input = GetIntInput();

            return input;
        }

        /// <summary>
        /// This method displays available balance.
        /// </summary>
        /// <param name="balance">User's available balance.</param>
        /// <param name="other">Displays extra text if other is true.</param>
        public static void CheckBalanceText(double balance, bool other=false)
        {
            if (!other)
            {
                Console.Clear();
                Console.WriteLine($"Here you go!\n");
            }
            
            Console.WriteLine($"Available Balance: {BankService.currency}{balance}");
            Console.WriteLine();
        }

        /// <summary>
        /// This method gets the deposit amount.
        /// </summary>
        /// <returns>Returns the amount to be deposited.</returns>
        public static double DepositText()
        {
            Console.Clear();
            Console.WriteLine("How much do you want to deposit?\n");
            double amount = GetDoubleInput(amountText);

            return amount;
        }

        /// <summary>
        /// This method gets the withdrawal amount.
        /// </summary>
        /// <returns>Returns the amount to be withdrawn.</returns>
        public static double WithdrawText()
        {
            Console.Clear();
            Console.WriteLine("How much do you want to withdraw?\n");
            double amount = GetDoubleInput(amountText);

            return amount;
        }

        /// <summary>
        /// This method gets transfer details.
        /// </summary>
        /// <returns>Returns a dictionary containing the transfer informations - amount and reciever's account number.</returns>
        public static Dictionary<string, dynamic> TransferText()
        {
            Console.Clear();
            Console.WriteLine("How much do you want to transfer?\n");
            double amount = GetDoubleInput(amountText, false);
            Console.WriteLine();

            Console.WriteLine("Who do you want to transfer the money to?\n");
            int accountNumber = GetIntInput("Account number: ");

            return new Dictionary<string, dynamic>
            {
                {"amount", amount },
                {"accountNumber", accountNumber },
            };
        }

        /// <summary>
        /// This method displays transfer confirmation and recives input.
        /// </summary>
        /// <param name="recieverName">Reciever's full name</param>
        /// <param name="amount">Amount to be transfered.</param>
        /// <returns>Returns the value for confirmation. 1 - Yes, 2 - No.</returns>
        public static int TransferConfirmation(string recieverName, double amount)
        {
            Console.WriteLine($"Are you sure you want to send {BankService.currency}{amount} to {recieverName}?");

            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            Console.WriteLine();

            int input = GetIntInput();

            return input;
        }

        /// <summary>
        /// This method displays back to main menu options. 
        /// </summary>
        /// <returns>Returns the value of input. 1 - Main Menu, 2 - Exit.</returns>
        public static int BackToMainMenuText()
        {
            Console.WriteLine("What do you want to do next?\n");
            Console.WriteLine("1. Main Menu");
            Console.WriteLine("2. Exit");
            Console.WriteLine();

            int input = GetIntInput();

            return input;
        }


        /// <summary>
        /// This method displays wrong input text.
        /// </summary>
        public static void WrongInput()
        {
            Console.WriteLine("Error! Wrong Input!");
        }

        /// <summary>
        /// This method clears console and displays account created successfully text.
        /// </summary>
        public static void RegistrationSuccessful()
        {
            Console.Clear();
            Console.WriteLine("Account created successfully!");
            Console.WriteLine();
        }

        /// <summary>
        /// This method clears console and displays registration failed text.
        /// </summary>
        public static void RegistrationFailed()
        {
            Console.Clear();
            Console.WriteLine("Error! Registration was NOT successful!");
        }

        /// <summary>
        /// This method clears console and displays login failed text.
        /// </summary>
        public static void LoginFailed()
        {
            Console.Clear();
            Console.WriteLine("Error! Unable to Log In with the provided credentials!");
        }

        /// <summary>
        /// This method clears console and displays deposit failed text.
        /// </summary>
        public static void DepositFailed()
        {
            Console.Clear();
            Console.WriteLine("Error! Deposit Failed!");
        }

        /// <summary>
        /// This method clears console and displays withdrawal failed text.
        /// </summary>
        public static void WithdrawalFailed()
        {
            Console.Clear();
            Console.WriteLine("Error! Withdrawal Failed!");
            Console.WriteLine();
        }

        /// <summary>
        /// This method clears console and dispalys deposit successful text.
        /// </summary>
        /// <param name="amount">Amount Deposited.</param>
        public static void DepositSuccessful(double amount)
        {
            Console.Clear();
            Console.WriteLine($"{BankService.currency}{amount} Deposited Successfully!");
            Console.WriteLine();
        }

        /// <summary>
        /// This method clears console and displays withdrawal successful text.
        /// </summary>
        /// <param name="amount">Amount Withdrawn.</param>
        public static void WithdawalSuccessful(double amount)
        {
            Console.Clear();
            Console.WriteLine($"{BankService.currency}{amount} Withdrawn Successfully!");
            Console.WriteLine();
        }

        /// <summary>
        /// This method clears console and displays transfer successful text.
        /// </summary>
        /// <param name="amount">Amount transferred.</param>
        public static void TransferSuccessful(double amount)
        {
            Console.Clear();
            Console.WriteLine($"{BankService.currency}{amount} Transfered Successfully!");
            Console.WriteLine();
        }

        /// <summary>
        /// This method clears console and displays transfer account not found error text.
        /// </summary>
        /// <param name="accountNumber">Account Number inputed.</param>
        public static void TransferAccountNotFoundError(int accountNumber)
        {
            Console.Clear();
            Console.WriteLine($"Could not find account with this account number - {accountNumber.ToString().PadLeft(10, '0')}");
            Console.WriteLine();
        }

        /// <summary>
        /// This method displays transfer failed text.
        /// </summary>
        public static void TransferFailed()
        {
            Console.WriteLine("Error! Transfer Failed!");
            Console.WriteLine();
        }
    }
}
