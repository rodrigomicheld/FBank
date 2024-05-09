using Domain.Entities;
using Domain.Enums;


namespace Fbank.IntegrationTests.Builders.Entities
{
    public class TransactionBuilder
    {
        private Guid? _id;
        private TransactionType? _transactionType;
        private decimal? _value;
        private Guid _accountId;
        private FlowType? _flowType;
        private Guid? _accountIdDestination; 
        private DateTime? _dateTransaction; 

        public Transaction Build() 
        {
            return new Transaction
            {
                Id = _id ?? Guid.NewGuid(),
                TransactionType = _transactionType ?? TransactionType.DEPOSIT,
                Value = _value ?? 100,
                AccountId = _accountId,
                AccountToId = _accountIdDestination,
                FlowType = _flowType ?? FlowType.INPUT,
                CreateDateAt = _dateTransaction ?? DateTime.Now,
            };
        }

        public TransactionBuilder WithDate(DateTime date)
        {
            _dateTransaction = date;
            return this;
        }

        public TransactionBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public TransactionBuilder WithTransactionType(TransactionType transactionType) 
        { 
            _transactionType = transactionType;
            return this;
        }

        public TransactionBuilder WithAccountId(Guid accountId)
        {
            _accountId = accountId;
            return this;
        }

        public TransactionBuilder WithAccountIdDestination(Guid accountIdDestination)
        {
            _accountIdDestination = accountIdDestination;
            return this;
        }

        public TransactionBuilder WithFlowType (FlowType flowType)
        {
            _flowType = flowType;
            return this;
        }
        public TransactionBuilder WithValue(decimal value)
        {
            _value = value;
            return this;
        }
    }
}