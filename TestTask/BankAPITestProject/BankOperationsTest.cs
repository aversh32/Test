using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAPITestProject.Models;
using BankAPI.Models;
using BankAPI.Models.Repositiry;

namespace BankAPITestProject
{
    [TestClass]
    public class BankOperationsTest
    {
        OrderOperations orderoper = new OrderOperations();
        BankOperations bankoper = new BankOperations();
        IRepository frepo = new FakeRepository();
        Card[] cards = new Card[]
            {
                new Card() { card_number = "2222",  expiry_month="12", expiry_year="2017", cvv="123", cardholder_name="wdfsfg" },
                new Card() { card_number = "1234",  expiry_month="11", expiry_year="2018", cvv="567", cardholder_name="ANDREY"},
                new Card() { card_number = "9999",  expiry_month="01", expiry_year="2018", cvv="532asd", cardholder_name="Andrey"},
            };
        [TestMethod]
        public void CorrectGetCardByNumberTest()
        {
                var card = frepo.GetCard("1234");
            Assert.IsNotNull(card);
        }

        [TestMethod]
        public void IncorrectGetCardByNumberTest()
        {
            var card = frepo.GetCard("12345678");
            Assert.IsNull(card);
        }

        [TestMethod]
        public void CorrectCheckCardExist()
        {
            Card usercard=cards[0];
            var card = frepo.GetCard(usercard.card_number);
            if (card == null)
            {
                throw new Exception("Card not Found");
            }
            else if (!(card.card_number == usercard.card_number && card.expiry_month == usercard.expiry_month && card.expiry_year == usercard.expiry_year && card.cvv == usercard.cvv && card.cardholder_name.ToUpper() == usercard.cardholder_name.ToUpper()))
                 throw new Exception("Wrong Card Data");
            Assert.IsNotNull(card);
        }

        [TestMethod, ExpectedException(typeof(Exception))]
        public void IncorrectCheckCardExist()
        {
            Card usercard = cards[0];
            usercard.cvv = "568";
            var card = frepo.GetCard(usercard.card_number);
            if (card == null)
            {
                throw new Exception("Card not Found");
            }
            else if (!(card.card_number == usercard.card_number && card.expiry_month == usercard.expiry_month && card.expiry_year == usercard.expiry_year && card.cvv == usercard.cvv && card.cardholder_name.ToUpper() == usercard.cardholder_name.ToUpper()))
                throw new Exception("Wrong Card Data");
        }

    }
}
