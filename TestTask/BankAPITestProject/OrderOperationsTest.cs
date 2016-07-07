using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAPI.Models.Repositiry;
using Moq;
using BankAPI.Controllers;
using BankAPI.Models;
using System.Collections.Generic;

namespace BankAPITestProject
{
    [TestClass]
    public class OrdersTests
    {
        [TestMethod]
        public void CorrectFindOrder()
        {

            // Arrange           
            OrderOperations orderoper = new OrderOperations();
            // Act
            Order order = orderoper.FindOrder(1);

            // Assert
            Assert.IsNotNull(order);
        }
    }
}


