using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BankAPI.Models;
using System.Web.Mvc;
using System.Collections.Specialized;
using System.Net.Http.Formatting;
using System.Data.Entity;

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
            new Card { card_number = "2222", limit = 1000, expiry_month="12", expiry_year="1233", cvv="123", cardholder_name="wdfsfg" },
            new Card { card_number = "1234", limit = -1, expiry_month="11", expiry_year="1111", cvv="567", cardholder_name="Andrey"},
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
        public IHttpActionResult PostPay(int id, [FromBody] FormDataCollection values )
        {
            var order = db.Orders.Find(id);           
            string card_number = values["c_number"];
            string expiry_month = values["exp_month"];
            string expiry_year = values["exp_year"];
            string cvv = values["cvv"];
            string cardholder_name = values["cardholder"];
            Card curCard = GetCard(card_number, expiry_month, expiry_year, cvv, cardholder_name);
            if(curCard == null)
            {
                return Content(HttpStatusCode.OK, "Card not Found");
            }
            
            bool isEnough = CheckLimit(order.amount_kop, card_number);
            if (!isEnough)
            {
                return Ok("Not Enough money   " + GetLimit(card_number));
            }
            //else return Ok("Rich bitch");
            //return Ok(cvv);
            try
            {
                PayBank(curCard, order);
            }
            catch
            {
                return Ok("Bank Error");
            }
            
            order.card_number = card_number;
            order.status = "Success";
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return Ok("success");
        }

        public IHttpActionResult GetStatus (int id)
        {
            var order = db.Orders.Find(id);
           if(order == null)
            {
                return NotFound();
            }
            else return Ok(order.status);
        }

        public IHttpActionResult GetRefund(int id)
        {
            var order = db.Orders.Find(id);
            if(order == null)
            {
                return Ok("Order not Found");
            }
            try
            {
                Card card = GetCard(order.card_number);
                card.limit += (double)order.amount_kop / 100;
            }
            catch
            {
                return Ok("Bank Error");
            }
            return Ok();
        }

        public bool CheckLimit(int amount, string number)
        {
            double limit = GetLimit(number);
            if (limit == 0 || limit - (double)amount/100 > 0)
                return true;
            else return false;
        }

        public double GetLimit(string number)
        {
            foreach(Card card in cards)
            {
                if (card.card_number == number)
                    return card.limit;
            }
            return -1;
        }

        public Card GetCard(string card_number, string expiry_month, string expiry_year, string cvv, string cardholder_name)
        {
            foreach (Card card in cards)
            {
                if (card.card_number == card_number && card.expiry_month==expiry_month && card.expiry_year==expiry_year && card.cvv == cvv && card.cardholder_name==cardholder_name)
                    return card;
            }
            return null;
        }

        public Card GetCard(string card_number)
        {
            foreach (Card card in cards)
            {
                if (card.card_number == card_number )
                    return card;
            }
            return null;
        }

        public IHttpActionResult GetCardLimit()
        {
            return Ok(GetLimit("2222"));
        }

        public void PayBank(Card card, Order order)
        {
            if (card.limit >= 0)
                card.limit -= (double)order.amount_kop / 100;
        }
    }
}
