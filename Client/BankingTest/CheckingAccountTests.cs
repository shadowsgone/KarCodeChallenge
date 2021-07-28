using BankingTest.Models;
using BankingTest.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace BankingTest
{
    [TestClass]
    public class CheckingAccountTests
    {
        private readonly BankingService _bankingService;

        public CheckingAccountTests()
        {
            _bankingService = new BankingService();
        }

        [TestMethod]
        public async Task Deposit()
        {
            try
            {
                var sucessful = await _bankingService.SetupBankAsync(AccountTypes.Checking ,10000.0M);
                Assert.IsTrue(sucessful);

                var balance = await _bankingService.Deposit(1000);
                Assert.AreEqual(balance, 11000.0M);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task Withdrawal()
        {
            try
            {
                var sucessful = await _bankingService.SetupBankAsync(AccountTypes.Checking, 10000.0M);
                Assert.IsTrue(sucessful);

                var balance = await _bankingService.Withdrawal(1000);
                Assert.AreEqual(balance, 9000.0M);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public async Task Transfer()
        {
            try
            {
                var sucessful = await _bankingService.SetupBankAsync(AccountTypes.Checking, 10000.0M);
                Assert.IsTrue(sucessful);

                var balance = await _bankingService.Transfer(1000);
                Assert.AreEqual(balance, 9000.0M);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
