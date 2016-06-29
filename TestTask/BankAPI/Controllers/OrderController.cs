using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BankAPI.Models;
using System.Web.Mvc;


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
        Card[] cards = new Card[]
        {
            new Card { card_number = "2222", limit = 1000 },
            new Card { card_number = "1234", limit = 0},
        };
        OrderContext db = new OrderContext();
        

        public IEnumerable<Order> GetAllOrders()
        {
            return db.Orders;
        }

        public IHttpActionResult GetOrder(int id)
        {
            var order = db.Orders.FirstOrDefault((p) => p.order_id == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult PostPay(int id)
        {
            var args = Request.RequestUri;
            var order = db.Orders.Find(id);
             bool isEnough = CheckLimit(order.amount_kop, cards[0].card_number);
            /*if (!isEnough)
            {
                return Ok("Недостаточно средств   " + GetLimit(cards[0].card_number));
            }
           else return Ok("Богато живёте");*/
            return Ok(args);
            
        }

        public IHttpActionResult GetStatus (int id)
        {
            var order = db.Orders.Find(id);
           
            return Ok(order.status);
        }

        public IHttpActionResult GetRefund(int id)
        {
            var order = db.Orders.Find(id);
            return Ok();
        }

        public bool CheckLimit(int amount, string number)
        {
            int limit = GetLimit(number);
            if (limit == 0 || limit - (double)amount/100 > 0)
                return true;
            else return false;
        }

        public int GetLimit(string number)
        {
            foreach(Card card in cards)
            {
                if (card.card_number == number)
                    return card.limit;
            }
            return -1;
        }

        public bool GetCard(string card_number, int expiry_month, int expiry_year, int cvv, string cardholder_name)
        {
            return true;
        }
    }
}
