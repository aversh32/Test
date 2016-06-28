using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BankAPI.Models;

namespace BankAPI.Controllers
{
    public class OrderController : ApiController
    {
        /*Order[] orders = new Order[]
        {
            new Order { order_id=1, amount_kop=1, cardholder_name="Test1", card_number="132", cvv="123", expiry_month="10", expiry_year="2017" },
            new Order { order_id=2, amount_kop=1, cardholder_name="Test2", card_number="133", cvv="123", expiry_month="10", expiry_year="2017" },
            new Order { order_id=3, amount_kop=1, cardholder_name="Test3", card_number="134", cvv="123", expiry_month="10", expiry_year="2017" },
        };*/
        OrderContext db = new OrderContext();
        

        public IEnumerable<Order> GetAllOrders()
        {
            return db.Orders;
        }

        public IHttpActionResult GetOrder(int order_id)
        {
            var order = db.Orders.FirstOrDefault((p) => p.order_id == order_id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        public IHttpActionResult Pay(int order_id, string card_number, string expiry_month, string expiry_year, string cvv, string cardholder_name, int amount_kop)
        {
            var order = new Order();
            return Ok();
        }

        public IHttpActionResult GetStatus (int order_id)
        {
            var order = db.Orders.Find(order_id);
           
            return Ok();
        }

        public IHttpActionResult Refund(int order_id)
        {
            var order = db.Orders.Find(order_id);
            return Ok();
        }

    }
}
