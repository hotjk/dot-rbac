﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Grit.CQRS
{
    public class UnitOfWork : IDisposable
    {
        private static TransactionOptions defaultTransactionOptions = new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead };
        private TransactionScope scope;

        public UnitOfWork()
        {
            ServiceLocator.EventBus.Clear();
            scope = new TransactionScope(TransactionScopeOption.RequiresNew, defaultTransactionOptions);
        }

        public UnitOfWork(TransactionScopeOption transactionScopeOption)
        {
            scope = new TransactionScope(transactionScopeOption, defaultTransactionOptions);
        }

        public void Dispose()
        {
            scope.Dispose();
        }

        public void Complete()
        {
            scope.Complete();
            Grit.CQRS.ServiceLocator.EventBus.FlushAll();
        }
    }
}
