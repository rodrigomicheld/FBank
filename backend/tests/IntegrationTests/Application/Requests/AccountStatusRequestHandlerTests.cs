using IntegrationTests.Builders.Entities;
using Application.Requests.Accounts;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests.Application.Requests
{
    public class AccountStatusRequestHandlerTests : ApplicationTestBase
    {

        [Fact]
        public async Task Should_return_InvalidOperationException_when_account_not_exit()
        {
            var request = new AccountStatusRequest { AccountNumber = 1, AccountStatus = AccountStatus.Active };

            Func<Task> handle = async () => await Handle<AccountStatusRequest, string>(request);

            await handle.Should().ThrowAsync<InvalidOperationException>().WithMessage("Account does not exist!");
        }

        [Fact]
        public async Task Should_return_InvalidOperationException_when_account_already_has_this_status()
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
                Status = AccountStatus.Active,
                AgencyId = agency.Id,
                ClientId = client.Id,
            };

            InsertOne(account);

            Func<Task> handle = async () => await Handle<AccountStatusRequest, string>(request);

            await handle.Should().ThrowAsync<InvalidOperationException>().WithMessage($"Account is {request.AccountStatus}!");
        }

        [Fact]
        public async Task Should_update_account_status_active()
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
                Status = AccountStatus.Inactive,
                AgencyId = agency.Id,
                ClientId = client.Id,
            };

            InsertOne(account);

            var response = await Handle<AccountStatusRequest, string>(request);

            Assert.Equal($"Account {account.Status} is successfully.", response);
        }

        [Fact]
        public async Task Should_update_account_status_inactive()
        {
            var request = new AccountStatusRequest { AccountNumber = 1, AccountStatus = AccountStatus.Inactive };

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

            var response = await Handle<AccountStatusRequest, string>(request);

            Assert.Equal($"Account {account.Status} is successfully.", response);
        }
    }
}
