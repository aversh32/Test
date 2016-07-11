using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAPI.Models.Repositiry;
using Moq;
using BankAPI.Controllers;
using BankAPI.Models;
using System.Collections.Generic;
using BankAPITestProject.Models;

namespace BankAPITestProject
{
    [TestClass]
    public class OrdersTests
    {
        OrderOperations orderoper = new OrderOperations();
        //Repository repo = new Repository();
        FakeRepository frepo = new FakeRepository();

        [TestMethod]
        public void CorrectFindOrder()
        {

            // Arrange           
            OrderOperations orderoper = new OrderOperations();
            // Act
            // Order order = orderoper.FindOrder(1);
            var order = frepo.GetOrder(1);
            if (order == null)
                throw new Exception("Order not found");
            // Assert
            Assert.IsNotNull(order);
        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void IncorrectFindOrder()
        {

            // Arrange           
            OrderOperations orderoper = new OrderOperations();
            // Act
            // Order order = orderoper.FindOrder(1);
            var order = frepo.GetOrder(3);
            if (order == null)
                throw new Exception("Order not found");
            // Assert

        }
    }
}


