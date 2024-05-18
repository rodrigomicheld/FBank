using IntegrationTests.Builders.Entities;
using MediatR;
using Domain.Entities;
using Domain.Enums;
using Application.Requests.Transactions;

namespace IntegrationTests.Application.Requests
{
    public class UpdateBalanceAccountRequestHandlerTests : ApplicationTestBase
    {
        private Guid PrepareScenarioToTest()
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
                Status = AccountStatus.Active,
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

            var request = new UpdateBalanceAccountRequest
            {
                AccountId =accountId,
                FlowType = FlowType.INPUT,
                Value = 10
            };

            var ex = await Assert.ThrowsAsync<Exception>(() => Handle<UpdateBalanceAccountRequest, Unit>(request));
            Assert.Contains("Account not found", ex.Message);
        }

        [Fact]
        public async Task ShouldReturnAnException_WhenValueLessThanOrEqual0()
        {
            Guid accountId = PrepareScenarioToTest();

            var request = new UpdateBalanceAccountRequest
            {
                AccountId =accountId,
                FlowType = FlowType.OUTPUT,
                Value = 0
            };
           
            var ex = await Assert.ThrowsAsync<Exception>(() => Handle<UpdateBalanceAccountRequest, Unit>(request));
            Assert.Contains("Value to be updated cannot be less than or equal to zero", ex.Message);
        }
    }
}
