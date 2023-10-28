using Azure.Core;
using Fbank.IntegrationTests.Builders.Entities;
using FBank.Application.Requests;
using FBank.Application.ViewMoldels;
using FBank.Domain.Entities;
using FBank.Domain.Enums;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Reflection.Metadata;

namespace Fbank.IntegrationTests.Application.Requests
{
    public class DepositMoneyAccountHandlerTests : ApplicationTestBase
    {
        public DepositMoneyAccountHandlerTests(WebApplicationFactory<Program> factory) : base(factory)
        {           
        }

        private void PrepareScenarioToTest()
        {
            var request = new AccountStatusRequest { AccountNumber = 1, AccountStatus = AccountStatusEnum.Active };

            Guid bankId = Guid.NewGuid();

            InsertOne(new BankBuilder().WithCode(1).WithId(bankId).Build());

            var agency = new AgencyBuilder().WithCode(1).WithBankId(bankId).Build();
            InsertOne(agency);

            var client = new ClientBuilder().Build();
            InsertOne(client);

            var account = new Account
            {
                Id = Guid.NewGuid(),
                Status = AccountStatusEnum.Inactive,
                AgencyId = agency.Id,
                ClientId = client.Id,
            };

            InsertOne(account);
        }

        [Fact]
        public async Task Should_return_Deposit_Did()
        {
            PrepareScenarioToTest();

            var request = new DepositMoneyAccountRequest { AccountNumber = 1,Value=10 };
            
            var response = await Handle<DepositMoneyAccountRequest, TransactionViewModel>(request);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_return_Deposit_NotDid()
        {
            PrepareScenarioToTest();

            var request = new DepositMoneyAccountRequest { AccountNumber = 0, Value = 10 };
            
            Func<Task> handle = async () => await Handle<DepositMoneyAccountRequest, TransactionViewModel>(request);

            await handle.Should().ThrowAsync<System.Exception>().WithMessage("Conta não encontrada");
        }

        
    }
}
