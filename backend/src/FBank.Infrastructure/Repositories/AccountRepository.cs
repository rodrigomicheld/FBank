﻿using FBank.Application.Interfaces;
using FBank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FBank.Infrastructure.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(DataBaseContext context) : base(context)
        {
        }

        public override Account SelectOne(Expression<Func<Account, bool>> filter = null)
        {
            IQueryable<Account> query = context.Accounts;

            query = query
                .Include(account => account.Agency)
                .Include(account => account.Transactions)
                .Include(account => account.Client);

            return query.Where(filter).FirstOrDefault();
        }

    }
}
