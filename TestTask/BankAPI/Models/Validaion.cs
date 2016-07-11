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