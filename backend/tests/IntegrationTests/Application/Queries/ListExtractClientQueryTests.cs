using IntegrationTests.Builders.Entities;
using Application.Queries.Accounts;
using Application.ViewMoldels;
using Domain.Common;
using Domain.Common.Filters;
using Domain.Enums;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests.Application.Queries
{
    public class ListExtractClientQueryTests : ApplicationTestBase
    {
        [Theory]
        [InlineData(1, 10)]
        [InlineData(2, 10)]
        [InlineData(10, 10)]
        [InlineData(1, 20)]
        [InlineData(3, 20)]
        [InlineData(2, 50)]
        public async Task Should_paginate_transactions(int page, int pageSize)
        {
            Guid clientId = Guid.NewGuid();
            Guid bankId = Guid.NewGuid();
            Guid agencyId = Guid.NewGuid();
            Guid accountId = Guid.NewGuid();

            InsertOne(new BankBuilder().WithCode(1).WithId(bankId).Build());
            InsertOne(new AgencyBuilder().WithId(agencyId).WithCode(1).WithBankId(bankId).Build());
            InsertOne(new ClientBuilder().WithId(clientId).Build());
            InsertOne(new AccountBuilder().WithClientId(clientId).WithAgencyId(agencyId).WithId(accountId).Build());

            InsertMany(Enumerable.Range(1, 99).Select(code => new TransactionBuilder()
                .WithAccountId(accountId)
                .Build()));

            var paginationResponse = await Handle<ListExtractClientQuery, PaginationResponse<ClientExtractViewModel>>(
                new ListExtractClientQuery
                {
                    FilterClient = new FilterClient()
                    {
                        _page = page,
                        _size = pageSize,
                        NumberAccount = 1,
                        FinalDate = DateTime.Now.AddDays(1),
                    }
                });

            var totalPages = (int)Math.Ceiling(99 / (double)pageSize);

            paginationResponse.TotalItems.Should().Be(99);
            paginationResponse.TotalPages.Should().Be(totalPages);
            paginationResponse.CurrentPage.Should().Be(page);

            var transaction = Enumerable.Range(1, 99).ToList();
            var transactionsWait = transaction.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        [Fact]
        public async Task Should_list_transaction_of_client()
        {
            Guid clientId = Guid.NewGuid();
            Guid clientIdTo = Guid.NewGuid();
            Guid bankId = Guid.NewGuid();
            Guid agencyId = Guid.NewGuid();
            Guid accountId = Guid.NewGuid();
            Guid accountIdTo = Guid.NewGuid();

            InsertOne(new BankBuilder().WithCode(1).WithId(bankId).Build());
            InsertOne(new AgencyBuilder().WithId(agencyId).WithCode(1).WithBankId(bankId).Build());
            InsertOne(new ClientBuilder().WithId(clientId).Build());
            InsertOne(new ClientBuilder().WithId(clientIdTo).WithDocument("123").Build());
            InsertOne(new AccountBuilder().WithClientId(clientId).WithAgencyId(agencyId).WithId(accountId).Build());
            InsertOne(new AccountBuilder().WithClientId(clientIdTo).WithAgencyId(agencyId).WithId(accountIdTo).Build());

            InsertOne(new TransactionBuilder()
                .WithAccountId(accountId)
                .WithTransactionType(TransactionType.DEPOSIT)
                .Build());

            InsertOne(new TransactionBuilder()
                .WithAccountId(accountId)
                .WithAccountIdDestination(accountIdTo)
                .WithValue(10)
                .WithDate(DateTime.Now.AddMinutes(10))
                .WithTransactionType(TransactionType.TRANSFER)
                .Build());

            var paginationResponse = await Handle<ListExtractClientQuery, PaginationResponse<ClientExtractViewModel>>(
                new ListExtractClientQuery
                {
                    FilterClient = new FilterClient()
                    {
                        NumberAccount = 1,
                        InitialDate = DateTime.Now.Date,
                        FinalDate = DateTime.Now.AddDays(1),
                    }
                });

            paginationResponse.TotalItems.Should().Be(2);

            paginationResponse.Data.Where(x=> x.Description.Contains("Transferência")).Count().Should().Be(1);
            paginationResponse.Data.Where(x=> x.Description.Contains("Transferência")).Select(x=> x.Amount).First().Should().Be("10,00");

            paginationResponse.Data.Where(x => x.Description.Contains("Depósito")).Count().Should().Be(1);
            paginationResponse.Data.Where(x => x.Description.Contains("Depósito")).Select(x => x.Amount).First().Should().Be("100,00");
        }
    }
}
