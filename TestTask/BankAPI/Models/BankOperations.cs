using BankAPI.Models.Repositiry;
using Microsoft.Net.Http.Server;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BankAPI.Models
{
     class BankOperations
    {
       // OrderContext db = new OrderContext();
        Repository repo = new Repository();
        public  void PayBank(string number, Order order)
        {
            var card = repo.GetCard(number);
            if (card.limit >= 0)
            {
                card.limit -= (double)order.amount_kop / 100;
            }
            order.card_number = card.card_number;
            order.status = "Success";
            try
            {
                /*db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();*/
                /*   repo.UpdateOrder(order);
                   repo.UpdateCard(card);
                   repo.Save();*/
                repo.Transaction(order, card);
            }
            catch
            {
                throw new BankErrorException("Bank Error");
            }
         }
        
        private Card GetCardByNumber(string card_number)
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
            else if (card.card_number == usercard.card_number && card.expiry_month == usercard.expiry_month && card.expiry_year == usercard.expiry_year && card.cvv == usercard.cvv && card.cardholder_name == usercard.cardholder_name)
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
            try
            {
                card = GetCardByNumber(order.card_number);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            try
            {
                card.limit += (double)order.amount_kop / 100;
                order.status = "Returned";
                /*db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();*/
                /* repo.UpdateOrder(order);
                 repo.UpdateCard(card);
                 repo.Save();*/
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
            //      Validaion.TryCardValidate(card);
            Validaion.ModelValidate(card);
            CheckCardExist(card);
            CheckCardLimit(order.amount_kop, card.card_number);
            PayBank(card.card_number, order);
            APILog.LogWrite("Success");
            return;        
        }    
    }

    public class BankErrorException : Exception
    {
        public BankErrorException() { }
        public BankErrorException(string message) : base(message) { }
    }
}