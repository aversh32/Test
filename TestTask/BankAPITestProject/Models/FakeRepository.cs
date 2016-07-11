using BankAPI.Models;
using BankAPI.Models.Repositiry;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAPITestProject.Models
{
    public class FakeRepository : IRepository
    {
        //private OrderContext context = new OrderContext();
        private FakeContext db;
       

        public FakeRepository()
        {
            this.db = new FakeContext();
            Card[] cards = new Card[]
            {
                new Card() { card_number = "2222",  expiry_month="12", expiry_year="2017", cvv="123", cardholder_name="wdfsfg" },
                new Card() { card_number = "1234",  expiry_month="11", expiry_year="2018", cvv="567", cardholder_name="ANDREY"},
                new Card() { card_number = "9999",  expiry_month="01", expiry_year="2018", cvv="532asd", cardholder_name="Andrey"},
            };
            foreach (Card card in cards)
                db.Cards.Add(card);
            Order[] orders = new Order[]
            {
                new Order() {order_id=1, amount_kop=123, status = "Success" },
                new Order() {order_id=2, amount_kop=10000, status = "Success" },
            };
            foreach (Order order in orders)
                db.Orders.Add(order);
        }
        public IEnumerable<Order> GetOrderList()
        {
            return db.Orders;
        }

        public Order GetOrder(int id)
        {
            return db.Orders.Find(o => o.order_id == id);
        }

        public Card GetCard(string number)
        {
            return db.Cards.Find(c => c.card_number == number);
        }

        public void UpdateOrder(Order o)
        {
            //db.Entry(o).State = EntityState.Modified;
        }

        public void UpdateCard(Card c)
        {
            //db.Entry(c).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Order o = db.Orders.Find(or => or.order_id == id);
            if (o != null)
                db.Orders.Remove(o);
        }

        public void Delete(string number)
        {
            Card c = db.Cards.Find(ca => ca.card_number == number);
            if (c != null)
                db.Cards.Remove(c);
        }

        public void Save()
        {
           // db.SaveChanges();
        }

        public IEnumerable<Card> Cards
        {
            get { return db.Cards; }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
           
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Transaction(Order o, Card c)
        {
            
        }
    }
}
