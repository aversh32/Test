using BankAPI.Models.Repositiry;
using Microsoft.Net.Http.Server;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BankAPI.Models
{
     public class BankOperations
    {
        // OrderContext db = new OrderContext();
        //Repository repo = new Repository();
        Repository repo=new Repository();

        public  void PayBank(string number, Order order)
        {
            var card = repo.GetCard(number);
            card.limit -= (double)order.amount_kop / 100;
            order.card_number = card.card_number;
            order.status = "Success";
            try
            {
                repo.Transaction(order, card);
            }
            catch
            {
                throw new BankErrorException("Bank Error");
            }
         }
        
        public Card GetCardByNumber(string card_number)
        {
            try
            {
                var card = repo.GetCard(card_number);
                return card;
            }
            catch
            {
                throw new Exception("Card not Found");
            }    
         
        }

        public void CheckCardExist(Card usercard)
        {
            var card = repo.GetCard(usercard.card_number);
            if (card == null)
            {
                throw new Exception("Card not Found");
            }
            else if (card.card_number == usercard.card_number 
                    && card.expiry_month == usercard.expiry_month 
                    && card.expiry_year == usercard.expiry_year 
                    && card.cvv == usercard.cvv && card.cardholder_name.ToUpper() == usercard.cardholder_name.ToUpper())
                return;
            throw new Exception("Wrong Card Data");
        }

        public void CheckCardLimit(int amount, string number)
        {
            double limit = GetCardLimit(number);
            if (limit < 0 || limit - (double)amount / 100 >= 0)
                return ;
            else throw new Exception("Not Enough Money");
        }

        public double GetCardLimit(string number)
        {
            var card = repo.GetCard(number);
            if (card != null)
                return card.limit;
             throw new Exception ("Card Not Found") ;
        }

        public void OrderRefund(int id)
        {
            OrderOperations orderoper = new OrderOperations();
            Order order = orderoper.FindOrder(id);
            Card card;
            //if (order.status != "Success")
            //    throw new OrderErrorException("Order no success");
            card = GetCardByNumber(order.card_number);

            try
            {
                card.limit += (double)order.amount_kop / 100;
                order.status = "Returned";
                repo.Transaction(order, card);
            }
            catch
            {
                throw new BankErrorException("Bank Error"); 
            }
        }

        public void Payment(int id, Card card)
        {
            OrderOperations orderoper = new OrderOperations();
            Order order;
            order = orderoper.FindOrder(id);
            Validaion.ModelValidate(card);
           /* if (order.status != null)
                throw new OrderErrorException("Order not waiting for payment");*/
            CheckCardExist(card);
            CheckCardLimit(order.amount_kop, card.card_number);
            PayBank(card.card_number, order);       
        }    
    }

    public class BankErrorException : Exception
    {
        public BankErrorException() { }
        public BankErrorException(string message) : base(message) { }
    }
    public class OrderErrorException : Exception
    {
        public OrderErrorException() { }
        public OrderErrorException(string message) : base(message) { }
    }
}