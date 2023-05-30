using BankApplication;
using BankApplication.Models;
using BankApplication.Services;

namespace BankApplicationTests
{
    [TestClass]
    public class AccountServiceTests
    {
        [TestMethod]
        public void GetUserAccount_WithValidUserId()
        {
            int userId = 1;
            Account? account = AccountService.GetUserAccount(userId);

            Assert.IsInstanceOfType(account, typeof(Account));
        }

        [TestMethod]
        public void GetUserAccountFromAccountNumber_WithValidAccountNumber()
        {
            int accountNumber = 1808050001;
            Account? account = AccountService.GetUserAccountFromAccountNumber(accountNumber);

            Assert.IsInstanceOfType(account, typeof(Account));
        }

        [TestMethod]
        public void Deposit_WithValidAmount()
        {
            // Arrange
            int userId = 1;
            Account? account = AccountService.GetUserAccount(userId);

            double beginningBalance = account.Balance;
            double depositAmount = 5.0;
            double expected = beginningBalance + depositAmount;

            // Act
            Account? updatedAccount = AccountService.Deposit(depositAmount, account);

            // Assert
            double actual = updatedAccount.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not funded correctly");
        }


        [TestMethod]
        public void Withdraw_WithValidAmount()
        {
            // Arrange
            int userId = 1;
            Account? account = AccountService.GetUserAccount(userId);

            double beginningBalance = account.Balance;
            double withdrawalAmount = 5.0;
            double expected = beginningBalance - withdrawalAmount;

            // Act
            Account? updatedAccount = AccountService.Withdraw(withdrawalAmount, account);

            // Assert
            double actual = updatedAccount.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Amount not withdrawn correctly");
        }


        //[TestMethod]
        //public void Transfer_WithValidAmount()
        //{
        //    // Arrange
        //    int userId = 1;
        //    Account? account = AccountService.GetUserAccount(userId);

        //    double beginningBalance = account.Balance;
        //    double transferAmount = 5.0;
        //    double expected = beginningBalance - transferAmount;

        //    // Act
        //    Dictionary<string, dynamic> transferDetails =
        //    new Dictionary<string, dynamic>
        //    {
        //        { "amount", transferAmount },
        //        { "accountNumber", 1808050002 },
        //    };

        //    Account? updatedAccount = AccountService.Transfer(transferDetails, account);

        //    // Assert
        //    double actual = updatedAccount.Balance;
        //    Assert.AreEqual(expected, actual, 0.001, "Amount not transferred correctly");
        //}


        [TestMethod]
        public void UpdateAccount_WithValidAccount()
        {
            int userId = 1;
            Account? account = AccountService.GetUserAccount(userId);
            bool updated = AccountService.UpdateAccount(account);

            Assert.IsTrue(updated);
        }

    }
}