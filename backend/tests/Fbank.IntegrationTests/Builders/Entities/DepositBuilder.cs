using FBank.Application.Requests;
using FBank.Domain.Entities;

namespace Fbank.IntegrationTests.Builders.Entities
{
    public class DepositBuilder
    {
        private int? _accountNumber ;
        private decimal? _value;
        public DepositMoneyAccountRequest Build() 
        {
            return new DepositMoneyAccountRequest
            {
                AccountNumber = _accountNumber ?? 0 ,
                Value = _value ?? 0
            };
        }

        public DepositBuilder WithAccount(int Account)  
        {
            _accountNumber = Account;
            return this;
        }

        public DepositBuilder WithValue(decimal Value) 
        {
            _value= Value;
            return this;
        }
    }
}
