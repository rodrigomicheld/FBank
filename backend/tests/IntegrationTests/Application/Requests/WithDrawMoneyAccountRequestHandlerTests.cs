using IntegrationTests.Builders.Entities;
using Application.Requests.Transactions;
using Application.ViewMoldels;
using Domain.Entities;
using Domain.Enums;

namespace IntegrationTests.Application.Requests
{

    public class WithDrawMoneyAccountRequestHandlerTests : ApplicationTestBase
    {
        private Guid PrepareScenarioToTest(AccountStatus accountStatus = AccountStatus.Active)
        {
            Guid bankId = Guid.NewGuid();

            InsertOne(new BankBuilder().WithCode(1).WithId(bankId).Build());

            var agency = new AgencyBuilder().WithCode(1).WithBankId(bankId).Build();
            InsertOne(agency);

            var client = new ClientBuilder().Build();
            InsertOne(client);

            var account = new Account
            {
                Id = Guid.NewGuid(),
                Status = accountStatus,
                AgencyId = agency.Id,
                ClientId = client.Id,
            };

            InsertOne(account);

            return account.Id;
        }

        [Fact]
        public async Task ShouldReturnAnException_WhenAccountNotFound()
        {
            Guid accountId = Guid.NewGuid();

            var request = new WithDrawMoneyAccountRequest
            {
                AccountNumber = 0,
                Amount = 10
            };

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => Handle<WithDrawMoneyAccountRequest, TransactionViewModel>(request));
            Assert.Contains("Account not found!", ex.Message);
        }

        [Fact]
        public async Task ShouldReturnAnException_WhenAccountIsInactive()
        {
            Guid accountId = PrepareScenarioToTest(AccountStatus.Inactive);

            var request = new WithDrawMoneyAccountRequest
            {
                AccountNumber = 1,
                Amount = 10
            };

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => Handle<WithDrawMoneyAccountRequest, TransactionViewModel>(request));
            Assert.Contains("Account is Inactive!", ex.Message);
        }

        [Theory]
        [InlineData(30, 29)]
        [InlineData(100, 40)]
        [InlineData(50, 15)]
        [InlineData(1, 0)]
        public async Task ShouldReturnAnException_WhenTheAmountIsGreaterThanTheCurrentBalance(decimal withdraw, decimal depositValue)
        {
            Guid accountId = PrepareScenarioToTest();

            var requestDeposit = new DepositMoneyAccountRequest { AccountNumber = 1, Value = depositValue };
            var responseDeposit = await Handle<DepositMoneyAccountRequest, TransactionViewModel>(requestDeposit);

            var request = new WithDrawMoneyAccountRequest
            {
                AccountNumber = 1,
                Amount = withdraw
            };

            var ex = await Assert.ThrowsAsync<Exception>(() => Handle<WithDrawMoneyAccountRequest, TransactionViewModel>(request));
            Assert.Contains("Insufficient balance", ex.Message);
        }

        [Theory]
        [InlineData(30, 29)]
        [InlineData(100, 40)]
        [InlineData(50, 15)]
        [InlineData(2, 1)]
        public async Task ShouldReturnAnTransaction_WhenValueTheRequestedAmountPassesTheRules(decimal depositValue, decimal withdraw)
        {
            Guid accountId = PrepareScenarioToTest();

            var requestDeposit = new DepositMoneyAccountRequest { AccountNumber = 1, Value = depositValue };
            var responseDeposit = await Handle<DepositMoneyAccountRequest, TransactionViewModel>(requestDeposit);

            var request = new WithDrawMoneyAccountRequest
            {
                AccountNumber = 1,
                Amount = withdraw
            };

            var response = await Handle<WithDrawMoneyAccountRequest, TransactionViewModel>(request);
            Assert.Equal(withdraw, response.Amount);
            Assert.Equal(TransactionType.WITHDRAW, response.TransactionType) ;
            
            var account = GetEntities<Account>().FirstOrDefault(x => x.Id == accountId);
            Assert.True(depositValue - withdraw == account.Balance);
        }
    }
}