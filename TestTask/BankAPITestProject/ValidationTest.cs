using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BankAPITestProject
{
    [TestClass]
    public class ValidationTest
    {
        [TestMethod]
        public void CorrectCardValidate()
        {
            Card card = new Card();
            card.card_number = "1111222233334444";
            card.expiry_month = "12";
            card.expiry_year = "2017";
            card.cvv = "123";
            card.cardholder_name = "Andrey";

    //        Validaion.TryCardValidate(card);
            //Assert
        }

        [TestMethod, ExpectedException(typeof(CardValidationException))]
        public void IncorrectCardNumberValidate()
        {
            Card card = new Card();
            card.card_number = "1111r";
            card.expiry_month = "12";
            card.expiry_year = "2017";
            card.cvv = "123";
            card.cardholder_name = "Andrey";

     //       Validaion.TryCardValidate(card);
            //Assert          
        }

        [TestMethod, ExpectedException(typeof(CardValidationException))]
        public void IncorrectCardMonthFormat()
        {
            Card card = new Card();
            card.card_number = "1111";
            card.expiry_month = "12r";
            card.expiry_year = "2017";
            card.cvv = "123";
            card.cardholder_name = "Andrey";

      //      Validaion.TryCardValidate(card);
            //Assert          
        }

        [TestMethod, ExpectedException(typeof(CardValidationException))]
        public void IncorrectCardMonthValue()
        {
            Card card = new Card();
            card.card_number = "1111";
            card.expiry_month = "13";
            card.expiry_year = "2017";
            card.cvv = "123";
            card.cardholder_name = "Andrey";

       //     Validaion.TryCardValidate(card);
            //Assert          
        }

        [TestMethod, ExpectedException(typeof(CardValidationException))]
        public void IncorrectCardYearFormat()
        {
            Card card = new Card();
            card.card_number = "1111";
            card.expiry_month = "12";
            card.expiry_year = "2017r";
            card.cvv = "123";
            card.cardholder_name = "Andrey";

         //   Validaion.TryCardValidate(card);
            //Assert          
        }

        [TestMethod, ExpectedException(typeof(CardValidationException))]
        public void IncorrectCardYearValue()
        {
            Card card = new Card();
            card.card_number = "1111";
            card.expiry_month = "12";
            card.expiry_year = "2015";
            card.cvv = "123";
            card.cardholder_name = "Andrey";

           // Validaion.TryCardValidate(card);
            //Assert          
        }

        [TestMethod]
        public void IncorrectCardCvv()
        {
            Card card = new Card {card_number = "1111",expiry_month = "12", expiry_year="2017", cvv = "12erhd34", cardholder_name = "Andrey" };
            var context = new ValidationContext(card, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(card, context, results);
            /*card.card_number = "1111";
            card.expiry_month = "12";
            card.expiry_year = "2017";
            card.cvv = "1234";
            card.cardholder_name = "Andrey";*/

            //Validaion.TryCardValidate(card);
            Assert.IsFalse(isValid);          
        }

        [TestMethod]
        public void IncorrectTest()
        {

            Test user = new Test { User = "eafeger", Name = "Tetsgggggggggggggggg", Age = 10000 };
            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(user, context, results, true);
          
            //  ModelState.IsValid
            /*card.card_number = "1111";
            card.expiry_month = "12";
            card.expiry_year = "2017";
            card.cvv = "1234";
            card.cardholder_name = "Andrey";*/
            Assert.IsFalse(isValid);
            //Validaion.TryCardValidate(card);
            //Assert          
        }


    }
}
