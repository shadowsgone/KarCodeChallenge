using BankingTest.Models;
using BankingTest.Wrappers;
using System;
using System.Threading.Tasks;

namespace BankingTest.Services
{
    public class BankingService : IDisposable
    {
        private readonly UsersService _usersService;
        private readonly BanksService _banksService;
        private readonly AccountsService _accountsService;
        private readonly TransactionsService _transactionsService;

        private int UserId { get; set; }
        private int BankId { get; set; }
        private int AccountId { get; set; }

        public BankingService()
        {
            _usersService = new UsersService(new HttpClientWrapper(), "https://localhost:44316/users");
            _banksService = new BanksService(new HttpClientWrapper(), "https://localhost:44307/banks");
            _accountsService = new AccountsService(new HttpClientWrapper(), "https://localhost:44394/accounts");
            _transactionsService = new TransactionsService(new HttpClientWrapper(), "https://localhost:44320/transactions");
        }

        public async Task<bool> SetupBankAsync(AccountTypes accountType, decimal accountStartingBalance)
        {
            try
            {
                UserId = await _usersService.Create("Josh", "Test", true);
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to create user.");
            }

            try
            {
                BankId = await _banksService.Create("TestingBank");
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create Bank.");
            }

            try
            {
                AccountId = await _accountsService.Create("TestInvestment",
                    accountType, 
                    UserId,
                    BankId,
                    accountStartingBalance);

                await _banksService.AddAccount(BankId, AccountId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create Account.");
            }

            return true;
        }

        public Task<decimal> Deposit(int amount)
        {
            return ProcessTransaction(TransactionTypes.Deposit, amount);
        }

        public Task<decimal> Withdrawal(int amount)
        {
            return ProcessTransaction(TransactionTypes.Withdraw, amount);
        }

        public Task<decimal> Transfer(int amount)
        {
            return ProcessTransaction(TransactionTypes.Transfer, amount);
        }

        private async Task<decimal> ProcessTransaction(TransactionTypes type, decimal amount)
        {
            await _transactionsService.Create(UserId, AccountId, type, amount);
            return await _accountsService.GetBalance(AccountId);
        }

        public void Dispose()
        {
            _usersService?.Dispose();
            _banksService?.Dispose();
            _accountsService?.Dispose();
            _transactionsService?.Dispose();
        }
    }
}
