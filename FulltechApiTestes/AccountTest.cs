using FulltechAPI.Controller;
using FulltechAPI.Core.Entities;
using FulltechAPI.Core.Interfaces;
using Xunit;
using Microsoft.AspNetCore.Mvc;

using Moq;

using System.Security.Principal;

namespace FulltechApiTestes
{
    public class AccountTest
    {
        [Fact]
        public void CreateAccount_Returns_CreatedAtAction()
        {
            var mockRepository = new Mock<IAccountRepository>();
            var controller = new AccountController(mockRepository.Object);
            var account = new Account { AccountNumber = 1, AccountHolder = "Fred", Balance = 100 };

            var result = controller.CreateAccount(account) as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal(account, result.Value);
        }

        [Fact]
        public void GetAccount_Returns_OkResult_With_Account()
        {
            var mockRepository = new Mock<IAccountRepository>();
            var controller = new AccountController(mockRepository.Object);
            var account = new Account { AccountNumber = 1, AccountHolder = "Fred", Balance = 100 };
            mockRepository.Setup(repo => repo.GetById(1)).Returns(account);

            var result = controller.GetAccount(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(account, result.Value);
        }

        [Fact]
        public void GetAllAccounts_Returns_OkResult_With_AccountsList()
        {
            var mockRepository = new Mock<IAccountRepository>();
            var controller = new AccountController(mockRepository.Object);
            var accounts = new List<Account> { new Account { AccountNumber = 1, AccountHolder = "Fred", Balance = 100 } };
            mockRepository.Setup(repo => repo.GetAll()).Returns(accounts);

            var result = controller.GetAllAccounts() as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(accounts, result.Value);
        }

        [Fact]
        public void DeleteAccount_Returns_NoContentResult()
        {
            var mockRepository = new Mock<IAccountRepository>();
            var controller = new AccountController(mockRepository.Object);
            var account = new Account { AccountNumber = 1, AccountHolder = "Fred", Balance = 100 };
            mockRepository.Setup(repo => repo.GetById(1)).Returns(account);

            var result = controller.DeleteAccount(1) as NoContentResult;

            Assert.NotNull(result);
        }
    }
}