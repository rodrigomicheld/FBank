using Application.Requests.Login;
using Application.Requests.Transactions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace UnitTests
{
    public static class FakeData
    {
        public static TokenRequest TokenRequest()
        {
            return new TokenRequest { Document = "129.166.530-74", Password = "123" };
        }

        public static Client Client()
        {
            return new Client
            {
                Id = Guid.NewGuid(),
                Name = "Defatul name test",
                Document = "29209320042",
                DocumentType = PersonType.Person,
                Password = "123",
            };
        }

        public static Agency Agency()
        {
            return new Agency
            {
                Id = Guid.NewGuid(),
                Name = "Agency test",
                Code = 1
            };
        }

        public static Account Account(AccountStatus accountStatus = AccountStatus.Active)
        {
            return new Account
            {
                Id = Guid.NewGuid(),
                Client = Client(),
                Agency = Agency(),  
                Number = 1,
                Status = accountStatus,
                Balance = 100
            };
        }


        public static HttpContext ContextRequestWithLogin()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "FakeName"),
                new Claim(ClaimTypes.NameIdentifier, "FakeId"),
                new Claim("name", "Fake Test"),
                new Claim("Account", "1")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(x => x.User.Claims).Returns(claims);
            mockHttpContext.Setup(m => m.User.Identity.IsAuthenticated).Returns(true);

            return mockHttpContext.Object;
        }

        public static Transaction Transaction(DepositMoneyAccountRequest request, Account fakeAccount)
        {
            var transactionBank = new Transaction();
            transactionBank.TransactionType = TransactionType.DEPOSIT;
            transactionBank.FlowType = FlowType.INPUT;
            transactionBank.AccountToId = fakeAccount.Id;
            transactionBank.AccountId = fakeAccount.Id;
            transactionBank.Value = request.Value;
            transactionBank.Account = fakeAccount;
            return transactionBank;
        }
    }
}
