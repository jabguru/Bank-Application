using BankApplication;
using BankApplication.Models;
using BankApplication.Services;

namespace BankApplicationTests
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public void RegisterUser_WithValidDetails()
        {
            Dictionary<string, string> userDetails = new Dictionary<string, string>
            {
                {"name", "John Doe" },
                {"email", $"johndoe{DateTime.Now.Millisecond}@gmail.com" },
                {"password", "123456" },
            };

            User? user = UserService.RegisterUser(userDetails);

            Assert.IsInstanceOfType(user, typeof(User));
        }

        [TestMethod]
        public void LoginUser_WithValidDetails()
        {
            Dictionary<string, string> userDetails = new Dictionary<string, string>
            {
                {"email", $"brownjulius980@gmail.com" },
                {"password", "123456" },
            };

            User? user = UserService.LoginUser(userDetails);

            Assert.IsInstanceOfType(user, typeof(User));
        }

        [TestMethod]
        public void GetUser_WithValidUserId()
        {
            int userId = 1;

            User? user = UserService.GetUser(userId);

            Assert.IsInstanceOfType(user, typeof(User));
        }
    }
}
