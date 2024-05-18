using IntegrationTests.Builders.Entities;
using Application.Requests.Accounts;
using Application.Requests.Transactions;
using Application.ViewMoldels;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;

namespace IntegrationTests.Application.Requests
{
    public class DepositMoneyAccountHandlerTests : ApplicationTestBase
    {
        private void PrepareScenarioToTest(AccountStatus accountStatus = AccountStatus.Active)
        {
            var request = new AccountStatusRequest { AccountNumber = 1, AccountStatus = AccountStatus.Active };

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
        }

        [Fact]
        public async Task Should_return_Deposit_Did()
        {
            PrepareScenarioToTest();

            var request = new DepositMoneyAccountRequest { AccountNumber = 1, Value = 10 };

            var response = await Handle<DepositMoneyAccountRequest, TransactionViewModel>(request);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task ShouldReturnAnException_WhenTheAccountDoesNotExist()
        {
            PrepareScenarioToTest();

            var request = new DepositMoneyAccountRequest { AccountNumber = 0, Value = 10 };

            Func<Task> handle = async () => await Handle<DepositMoneyAccountRequest, TransactionViewModel>(request);

            await handle.Should().ThrowAsync<InvalidOperationException>().WithMessage("Account not found");
        }

        [Fact]
        public async Task ShouldReturnAnException_WhenTheAccountInactive()
        {
            PrepareScenarioToTest(AccountStatus.Inactive);

            var request = new DepositMoneyAccountRequest { AccountNumber = 1, Value = 10 };

            Func<Task> handle = async () => await Handle<DepositMoneyAccountRequest, TransactionViewModel>(request);

            await handle.Should().ThrowAsync<InvalidOperationException>().WithMessage("Account is Inactive!");
        }

        [Fact]
        public async Task ShouldReturnEmptyObject_WhenValueLessThanOrEqual0()
        {
            PrepareScenarioToTest();

            var request = new DepositMoneyAccountRequest { AccountNumber = 1, Value = 0 };

            var response = await Handle<DepositMoneyAccountRequest, TransactionViewModel>(request);

            Assert.Equal(0, response.Amount);
            Assert.Equal(TransactionType.PAYMENT, response.TransactionType);
            Assert.Equal(DateTime.MinValue, response.DateTransaction);
        }
    }
}
