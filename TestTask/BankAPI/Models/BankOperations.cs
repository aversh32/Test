using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BankAPI.Models
{
    public class BankOperations
    {
        OrderContext db = new OrderContext();
        public  void PayBank(string number, Order order)
        {
            var card = db.Cards.Find(number);
            if (card.limit >= 0)
            {
                card.limit -= (double)order.amount_kop / 100;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return;
            }
            order.card_number = card.card_number;
            order.status = "Success";
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
        }
        public Card GetCard(string card_number)
        {
            var card = db.Cards.Find(card_number);
            if (card != null)
                return card;
            return null;
        }
        public bool GetCard(string card_number, string expiry_month, string expiry_year, string cvv, string cardholder_name)
        {
            var card = db.Cards.Find(card_number);
            if (card == null)
            {
                return false;
            }
            else if (card.card_number == card_number && card.expiry_month == expiry_month && card.expiry_year == expiry_year && card.cvv == cvv && card.cardholder_name == cardholder_name)
                return true;
            return false;
        }

        public bool CheckLimit(int amount, string number)
        {
            double limit = GetLimit(number);
            if (limit < 0 || limit - (double)amount / 100 >= 0)
                return true;
            else return false;
        }

        public double GetLimit(string number)
        {
            var card = db.Cards.Find(number);
            if (card != null)
                return card.limit;

            return -1;
        }
    }
}