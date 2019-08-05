using DeepEqual.Syntax;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Telerik.JustMock.AutoMock;
using TestingHomework.Tests.TestBases;
using TestingHomework_Discounts.Accessors;
using TestingHomework_Discounts.Managers;
using Xunit;

namespace TestingHomework.Tests.Tests
{
    public class UserAdminTests : ManagerTestBase
    {
        MockingContainer<UserAdminManager> mockContainer = new MockingContainer<UserAdminManager>();

        UserAdminManager userAdminManager;

        public override void OnCleanup()
        {
            throw new NotImplementedException();
        }

        public override void OnInitialize()
        {
            userAdminManager = mockContainer.Instance;
        }

        [Test]
        public void GetUserById()
        {            
            var user = dataPrep.Users.Create();
            mockContainer.Arrange<IUserAdminAccessor>(accessor => accessor.GetUser(user.Id)).Returns(user);

            userAdminManager = mockContainer.Instance;

            var expectedUser = userAdminManager.GetUser(user.Id);

            user.ShouldDeepEqual(expectedUser);
        }

        [Test]
        public void SaveUser()
        {
            var user = dataPrep.Users.Create();
            mockContainer.Arrange<IUserAdminAccessor>(accessor => accessor.SaveUser(user)).Returns(user);

            userAdminManager = mockContainer.Instance;

            var expectedUser = userAdminManager.SaveUser(user);

            user.ShouldDeepEqual(expectedUser);
        }

        [Test]
        public void GetAllProducts()
        {
            // arrange
            var userList = dataPrep.Users.Create();
            int expectedItemCount = 5;
            var expectedItemList = dataPrep.Users.CreateManyForUsers(expectedItemCount, userList);
            mockContainer.Arrange<IUserAdminAccessor>(accessor => accessor.GetAllUsers()).Returns(expectedItemList);

            userAdminManager = mockContainer.Instance;

            var users = userAdminManager.GetAllUsers();

            expectedItemList.ShouldDeepEqual(users);


        }

    }
}

