﻿using BudgetPlanner.Data;
using BudgetPlanner.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Services
{
    public class TransactionService
    {
        private readonly Guid _userId;

        public TransactionService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateTransaction(TransactionCreate model)
        {
            var entity =
                new Transaction()
                {
                    UserId = _userId,
                    Name = model.Name,
                    Amount = model.Amount,
                    TransactionDate = model.TransactionDate,
                    MerchantName = model.MerchantName,
                    CategoryId = model.CategoryId,
                    ExcludeTransaction = model.ExcludeTransaction
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Transactions.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<TransactionListItem> GetTransactions()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Transactions
                        .Where(e => e.UserId == _userId)
                        .Select(
                            e =>
                                new TransactionListItem
                                {
                                    TransactionId = e.TransactionId,
                                    MerchantName = e.MerchantName,
                                    Amount = e.Amount,
                                    TransactionDate = e.TransactionDate,
                                    CategoryId = e.CategoryId
                                }
                                );
                return query.ToArray();
            }
        }

        public TransactionDetail GetTransactionById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Transactions
                        .Single(e => e.TransactionId == id && e.UserId == _userId);

                return
                    new TransactionDetail
                    {
                        TransactionId = entity.TransactionId,
                        Amount = entity.Amount,
                        TransactionDate = entity.TransactionDate,
                        MerchantName = entity.MerchantName,
                        CategoryId = entity.CategoryId,
                        ExcludeTransaction = entity.ExcludeTransaction
                    };
            }
        }

        public bool UpdateTransaction(TransactionEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Transactions
                        .Single(e => e.TransactionId == model.TransactionId && e.UserId == _userId);

                entity.Name = model.Name;
                entity.Amount = model.Amount;
                entity.TransactionDate = model.TransactionDate;
                entity.MerchantName = model.MerchantName;
                entity.CategoryId = model.CategoryId;
                entity.ExcludeTransaction = model.ExcludeTransaction;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteTransaction(int transactionId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Transactions
                    .Single(e => e.TransactionId == transactionId && e.UserId == _userId);

                ctx.Transactions.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
