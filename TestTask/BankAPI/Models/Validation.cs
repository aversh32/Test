using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankAPI.Models
{
    public class Validation
    {
        public void TryValidate(string card_number, string expiry_month, string expiry_year, string cvv, string cardholder_name)
        {
            int res;
            bool isInt = Int32.TryParse(card_number, out res);
            if(!isInt)
            {
                throw new CardException("card_number is not number");
            }
            isInt = Int32.TryParse(expiry_month, out res);
            if (!isInt)
            {
                throw new CardException("is not number expiry_month");
            }
            else if(res<1 || res>12)
            {
                throw new CardException("Wrong expiry_month");
            }
            isInt = Int32.TryParse(expiry_year, out res);
            if (!isInt)
            {
                throw new CardException("is not number expiry_year");
            }
            else if (res < 2016 )
            {
                throw new CardException("Wrong expiry_year");
            }
            isInt = Int32.TryParse(cvv, out res);
            if (!isInt)
            {
                throw new CardException("is not number cvv");
            }
            else if (res < 100 || res>999)
            {
                throw new CardException("Wrong cvv");
            }

        }
    }
    public class CardException : ApplicationException
    {
        public CardException() { }
        public CardException(string message) : base(message) { }
    }
}