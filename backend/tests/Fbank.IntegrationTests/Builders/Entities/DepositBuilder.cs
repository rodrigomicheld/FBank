using FBank.Application.Requests.Transactions;

namespace Fbank.IntegrationTests.Builders.Entities
{
    public class DepositBuilder
    {
        private int? _accountNumber;
        private decimal? _value;
        public DepositMoneyAccountRequest Build()
        {
            return new DepositMoneyAccountRequest
            {
                Account = _accountNumber ?? 0,
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
            _value = Value;
            return this;
        }
    }
}
