using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BankAPI.Models.Repositiry
{
    public interface IRepository : IDisposable
    {
        IEnumerable<Order> GetOrderList();
       // IEnumerable<Card> GetCardList();
        Order GetOrder(int id);
        Card GetCard(string number);
        //void Create(Order item);
        void UpdateOrder(Order item);
        void UpdateCard(Card item);
        void Delete(int id);
        void Save();
        void Transaction(Order o, Card c);
    }

    public class Repository: IRepository
    {
        //private OrderContext context = new OrderContext();
        private OrderContext db;
        public Repository()
        {
            this.db = new OrderContext();
        }
        public IEnumerable<Order> GetOrderList()
        {
           return db.Orders; 
        }

        public Order GetOrder(int id)
        {
            return db.Orders.Find(id);
        }

        public Card GetCard(string number)
        {
            return db.Cards.Find(number);
        }

        public void UpdateOrder(Order o)
        {
            db.Entry(o).State = EntityState.Modified;
        }

        public void UpdateCard(Card c)
        {
            db.Entry(c).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Order o = db.Orders.Find(id);
            if (o != null)
                db.Orders.Remove(o);
        }

        public void Delete(string number)
        {
            Card c = db.Cards.Find(number);
            if (c != null)
                db.Cards.Remove(c);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<Card> Cards
        {
            get { return db.Cards; }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Transaction(Order o, Card c)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Entry(o).State = EntityState.Modified;
                    db.Entry(c).State = EntityState.Modified;
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new BankErrorException("Bank Error");
                }
            }
        }
    }
}