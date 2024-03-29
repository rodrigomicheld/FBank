﻿using FBank.Application.Requests.Login;
using FBank.Domain.Entities;
using FBank.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace FBank.UnitTests
{
    public static class FakeData
    {
        public static TokenRequest TokenRequest()
        {
            return new TokenRequest { NumberAgency = 1, NumberAccount = 2, Password = "123" };
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

        public static Account Account()
        {
            return new Account
            {
                Id = Guid.NewGuid(),
                Client = Client(),
                Agency = Agency(),  
                Number = 1,
                Status = AccountStatus.Active,
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
    }
}
