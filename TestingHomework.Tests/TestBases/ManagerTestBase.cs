using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using TestingHomework.Tests.DataPrep;

namespace TestingHomework.Tests.TestBases
{
    public abstract class ManagerTestBase
    {
        protected DiscountsDataPrep dataPrep = new DiscountsDataPrep(false);
        TransactionScope _transactionScope;

        public abstract void OnInitialize();
        [SetUp]
        public virtual void TestInitialize()
        {
            dataPrep.EnsureDatastore();
            // transactionScope causes db changes to be rolled back at end of test
            _transactionScope = new TransactionScope();

            OnInitialize();
        }


        public abstract void OnCleanup();
        [TearDown]
        public virtual void TestCleanup()
        {
            OnCleanup();
            _transactionScope.Dispose();
        }

    }
}
