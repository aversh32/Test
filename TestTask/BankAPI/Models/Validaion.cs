using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace BankAPI.Models
{
   public static class Validaion
    {
        public static void TryCardValidate(Card card)
        {
            long num;
            int res;
            bool isInt = Int64.TryParse(card.card_number, out num);
            if(!isInt)
            {
                throw new CardValidationException("card_number is not number");
            }
            isInt = Int32.TryParse(card.expiry_month, out res);
            if (!isInt)
            {
                throw new CardValidationException("is not number expiry_month");
            }
            else if(res<1 || res>12)
            {
                throw new CardValidationException("Wrong expiry_month");
            }
            isInt = Int32.TryParse(card.expiry_year, out res);
            if (!isInt)
            {
                throw new CardValidationException("is not number expiry_year");
            }
            else if (res < 2016 )
            {
                throw new CardValidationException("Wrong expiry_year");
            }
            isInt = Int32.TryParse(card.cvv, out res);
            if (!isInt)
            {
                throw new CardValidationException("is not number cvv");
            }
            else if (res < 100 || res>999)
            {
                throw new CardValidationException("Wrong cvv");
            }

        }

        public static void ModelValidate(Card card)
        {

            var context = new ValidationContext(card, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(card, context, results, true);
            if(!isValid)
            {
                throw new CardValidationException("Card Data Error");
            }
        }
    }
    public class CardValidationException : Exception
    {
        public CardValidationException() { }
        public CardValidationException(string message) : base(message) { }
    }
}