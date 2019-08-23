using Accessors.DatabaseAccessors;
using DeepEqual.Syntax;
using Managers.LazyCollectionOfAllManagers;
using Ninject;
using NUnit.Framework;
using Shared.DatabaseContext;
using Shared.DatabaseContext.DBOs;
using Shared.DataContracts;
using Shared.DependencyInjectionKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock.AutoMock;
using Test.NUnitExtensions;
using Tests.DataPrep;
using Tests.ManagerTests;

namespace TestingHomework.Tests
{
    //[TestFixture_Prefixed(typeof(UserAdminManager), false)]
    [TestFixture_Prefixed(typeof(UserAdminManager), true)]
    public class UserAdminManagerTests : ManagerTestBase
    {
        UserAdminManager manager;
        MockingContainer<UserAdminManager> mockContainer = new MockingContainer<UserAdminManager>();

        public UserAdminManagerTests() : this(false) { }
        public UserAdminManagerTests(bool isIntegration = false)
        {
            // this constructor allows for integration test reuse
            if (isIntegration)
            {
                // Implicit self binding allows us to get a concrete class with fulfilled dependencies
                // https://github.com/ninject/ninject/wiki/dependency-injection-with-ninject
                Ninject.IKernel kernel = DependencyInjectionLoader.BuildKernel();
                manager = kernel.Get<UserAdminManager>();
                dataPrep = new DiscountsDataPrep(true);
            }
            else
            {
                manager = mockContainer.Instance;
                // dataPrep non-persistant by default in base class
            }
        }

        public override void OnCleanup()
        {
            using (TestingHomeworkContext db = new TestingHomeworkContext())
            {
                foreach (UserDTO us in db.Users)
                {
                    us.IsActive = false;
                    db.SaveChanges();
                }

             }
        }        

        public override void OnInitialize()
        {

        }

        [Test]
        public void GetUser()
        {
            //// arrange           
            User expectedUser = dataPrep.Users.CreateData();
            mockContainer.Arrange<IUserAccessor>(accessor => accessor.GetUser(expectedUser.Id)).Returns(expectedUser);
            //// act
            User actualUser = manager.GetUser(expectedUser.Id);
            ////assert
            expectedUser.ShouldDeepEqual(actualUser);
        }

        [Test]
        public void GetAllUsers()
        {
            // arrange
            //  User user = dataPrep.Users.Create();
            //IEnumerable<Cart> cart = dataPrep.Carts.CreateManyForList(1,false);

            int expectedItemCount = 5;
            IEnumerable<User> expectedUsers = dataPrep.Users.CreateManyForList(expectedItemCount);

            mockContainer.Arrange<IUserAccessor>(accessor => accessor.GetAllUsers()).Returns(expectedUsers);

            //// act
            IEnumerable<User> actualUsers = manager.GetAllUsers();

            ////assert
            Assert.AreEqual(expectedUsers.Count(), actualUsers.Count());
            expectedUsers.ShouldDeepEqual(actualUsers);
        }

        [Test]
        public void SaveUser()
        {
            // arrange
            User expectedUser = dataPrep.Users.CreateData(isPersisted: false);

            mockContainer.Arrange<IUserAccessor>(accessor => accessor.SaveUser(expectedUser)).Returns(new SaveResult<User>(expectedUser));

            // act
            SaveResult<User> actualUserResult = manager.SaveUser(expectedUser);
            User actualUser = actualUserResult.Result;

            //assert
            Assert.IsTrue(actualUserResult.Success);
            expectedUser.WithDeepEqual(actualUser).IgnoreSourceProperty((p) => p.Id);
        }

        [Test]
        public void UpdateUser()
        {
            // arrange
            User expectedUser = dataPrep.Users.CreateData();
            expectedUser.LastName = Guid.NewGuid().ToString();

            mockContainer.Arrange<IUserAccessor>(accessor => accessor.SaveUser(expectedUser)).Returns(new SaveResult<User>(expectedUser));

            // act
            SaveResult<User> actualUserResult = manager.SaveUser(expectedUser);
            User actualPromoCode = actualUserResult.Result;

            //assert
            Assert.IsTrue(actualUserResult.Success);
            Assert.AreEqual(expectedUser.Id, actualPromoCode.Id);
            expectedUser.WithDeepEqual(actualPromoCode);
        }

    }
}
