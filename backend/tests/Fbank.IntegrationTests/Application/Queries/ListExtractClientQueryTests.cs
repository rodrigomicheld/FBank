using FBank.Domain.Entities;
using Fbank.IntegrationTests.Builders.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FBank.Application.Requests;
using System.Reflection.Metadata;
using FBank.Application.Queries;
using FBank.Domain.Common;
using FBank.Application.ViewMoldels;
using FBank.Domain.Common.Filters;
using FluentAssertions;

namespace Fbank.IntegrationTests.Application.Queries
{
    public class ListExtractClientQueryTests : ApplicationTestBase
    {
        public ListExtractClientQueryTests(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData(1, 10)]
        [InlineData(2, 10)]
        [InlineData(10, 10)]
        [InlineData(1, 20)]
        [InlineData(3, 20)]
        [InlineData(2, 50)]
        public async Task Deve_fazer_a_paginacao_das_transacoesAsync(int page, int pageSize)
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

            var codigo = Enumerable.Range(1,99).ToList();
            var codigosesperados = codigo.Skip((page-1) * pageSize).Take(pageSize).ToList();
        }
    }
}
