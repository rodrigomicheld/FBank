using Application.Requests.Transactions;
using Application.ViewMoldels;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using IntegrationTests.Builders.Entities;

namespace IntegrationTests.Application.Requests
{
    public class TransferMoneyAccountRequestHandlerTests : ApplicationTestBase
    {
        private void PrepareScenarioToTest(AccountStatus accountStatus = AccountStatus.Active, bool createDestAccount = false)
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

            if (!createDestAccount) return;

            var clientDest = new ClientBuilder().WithId(Guid.NewGuid())
                                                .WithName("Cliente destinatário da transferencia")
                                                .WithDocument("71491247045")
                                                .Build();
            InsertOne(clientDest);

            var accountDest = new Account
            {
                Id = Guid.NewGuid(),
                Status = accountStatus,
                AgencyId = agency.Id,
                ClientId = clientDest.Id,
            };

            InsertOne(accountDest);
        }

        [Fact]
        public async Task ShouldReturnAnException_WhenOriginAccountNotFound()
        {
            PrepareScenarioToTest(createDestAccount: false);

            var request = new TransferMoneyAccountRequest
            {
                AccountNumberFrom = 15,
                AccountNumberTo = 2,
                Value = 10
            };

            Func<Task> handle = async () => await Handle<TransferMoneyAccountRequest, string>(request);

            await handle.Should().ThrowAsync<Exception>().WithMessage("Account not found");
        }

        [Fact]
        public async Task ShouldReturnAnException_WhenDestinationAccountNotFound()
        {
            PrepareScenarioToTest(createDestAccount: false);

            var request = new TransferMoneyAccountRequest
            {
                AccountNumberFrom = 1,
                AccountNumberTo = 2,
                Value = 10
            };

            Func<Task> handle = async () => await Handle<TransferMoneyAccountRequest, string>(request);

            await handle.Should().ThrowAsync<Exception>().WithMessage("Destination account not found");
        }

        [Fact]
        public async Task ShouldReturnAnException_WhenTheTransferDoesNotMeetTheRequirements()
        {
            PrepareScenarioToTest(accountStatus: AccountStatus.Inactive, createDestAccount: true);

            var request = new TransferMoneyAccountRequest
            {
                AccountNumberFrom = 1,
                AccountNumberTo = 1,
                Value = 0
            };

            var ex = await Assert.ThrowsAsync<Exception>(() => Handle<TransferMoneyAccountRequest, string>(request));
            Assert.Contains("Transfer to the same account is not permitted", ex.Message);
            Assert.Contains("Transfer amount cannot be less than or equal to zero", ex.Message);
            Assert.Contains("Account to is Inactive", ex.Message);
            Assert.Contains("Account from is Inactive", ex.Message);
        }

        [Theory]
        [InlineData(5, 6)]
        [InlineData(100, 200)]
        [InlineData(50, 250)]
        public async Task ShouldReturnAnException_WhenTheAmountIsGreaterThanTheCurrentBalance(decimal depositValue, decimal transfValue)
        {
            PrepareScenarioToTest(createDestAccount:true);


            var requestDeposit = new DepositMoneyAccountRequest { AccountNumber = 1, Value = depositValue };
            var responseDeposit = await Handle<DepositMoneyAccountRequest, TransactionViewModel>(requestDeposit);

            var request = new TransferMoneyAccountRequest
            {
                AccountNumberFrom = 1,
                AccountNumberTo = 2,
                Value = transfValue
            };

            var ex = await Assert.ThrowsAsync<Exception>(() => Handle<TransferMoneyAccountRequest, string>(request));
            Assert.Contains("Insufficient balance to make the transfer, current balance", ex.Message);
        }

        [Theory]
        [InlineData(10,5)]
        [InlineData(100,95)]
        [InlineData(550.25,250)]
        [InlineData(510,509)]
        public async Task ShouldReturnSuccess_WhenValueTheRequestedAmountPassesTheRules(decimal depositValue, decimal transfValue)
        {
            PrepareScenarioToTest(createDestAccount:true);

            var requestDeposit = new DepositMoneyAccountRequest { AccountNumber = 1, Value = depositValue };
            var responseDeposit = await Handle<DepositMoneyAccountRequest, TransactionViewModel>(requestDeposit);

            var request = new TransferMoneyAccountRequest
            {
                AccountNumberFrom = 1,
                AccountNumberTo = 2,
                Value = transfValue
            };

            var responseTransf = await Handle<TransferMoneyAccountRequest, string>(request);

            Assert.Equal("Successful transfer", responseTransf);
            Assert.Equal(depositValue, responseDeposit.Amount);
            Assert.Equal(TransactionType.DEPOSIT, responseDeposit.TransactionType);

            var accountOrigin = GetEntities<Account>().FirstOrDefault(x => x.Number == 1);
            var accountDestination = GetEntities<Account>().FirstOrDefault(x => x.Number == 2);

            Assert.Equal(depositValue-transfValue, accountOrigin.Balance);
            Assert.Equal(transfValue, accountDestination.Balance);
        }
    }
}
