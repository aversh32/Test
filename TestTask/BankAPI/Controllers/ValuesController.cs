using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BankAPI.Models;
using System.Data.Entity;

namespace BankAPI.Controllers
{
  //  [Authorize]
    public class ValuesController : ApiController
    {
        OrderContext db = new OrderContext();


        // GET api/values
        public IEnumerable<Order> GetOrders()
        {
            return db.Orders;
        }

        // GET api/values/5
        public Order GetOrder(int id)
        {
            Order order = db.Orders.Find(id);
            return order;
        }

        // POST api/values
        public void CreateOrder([FromBody]Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
        }

        // PUT api/values/5
        public void EditOrder(int id, [FromBody]Order order)
        {
            if(id == order.order_id)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            Order order = db.Orders.Find(id);
            if (order != null)
            {
                db.Orders.Remove(order);
                db.SaveChanges();
            }
        }
    }
}
