using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BankAPI.Models
{
     class BankOperations
    {
        OrderContext db = new OrderContext();
        public  void PayBank(string number, Order order)
        {
            var card = db.Cards.Find(number);
            if (card.limit >= 0)
            {
                card.limit -= (double)order.amount_kop / 100;
                order.card_number = card.card_number;
                order.status = "Success";
                try
                {
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch
                {
                    throw new Exception("Bank Error");
                }
            }
        }
        private Card GetCard(string card_number)
        {
            try
            {
                var card = db.Cards.Find(card_number);
                return card;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }    
         
        }
        public void GetCard(string card_number, string expiry_month, string expiry_year, string cvv, string cardholder_name)
        {
            var card = db.Cards.Find(card_number);
            if (card == null)
            {
                throw new Exception("Card not Found");
            }
            else if (card.card_number == card_number && card.expiry_month == expiry_month && card.expiry_year == expiry_year && card.cvv == cvv && card.cardholder_name == cardholder_name)
                return;
            throw new Exception("Wrong Card Data");
        }

        public void CheckLimit(int amount, string number)
        {
            double limit = GetLimit(number);
            if (limit < 0 || limit - (double)amount / 100 >= 0)
                return ;
            else throw new Exception("Not Enough Money");
        }

        public double GetLimit(string number)
        {
            var card = db.Cards.Find(number);
            if (card != null)
                return card.limit;
             throw new Exception ("Card Not Found") ;
        }
        public void OrderRefund(int id)
        {
            OrderOperations orderoper = new OrderOperations();
            Order order;
            try
            {
                order = orderoper.FindOrder(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            Card card;
            try
            {
                card = GetCard(order.card_number);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            try
            {
                card.limit += (double)order.amount_kop / 100;
                order.status = "Returned";
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                throw new Exception("Bank Error"); 
            }
        }

    }
}