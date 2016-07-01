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
        OrderContext db = new OrderContext();
        BankOperations bankoper = new BankOperations();
        OrderOperations orderoper = new OrderOperations();
        Validation valid = new Validation();
        public IEnumerable<Order> GetAllOrders()
        {
            return db.Orders;
        }

        public IEnumerable<Card> GetAllCards()
        {
            return db.Cards;
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
            var order = orderoper.FindOrder(id);           
            string card_number = values["c_number"];
            string expiry_month = values["exp_month"];
            string expiry_year = values["exp_year"];
            string cvv = values["cvv"];
            string cardholder_name = values["cardholder"];
            valid.TryValidate(card_number, expiry_month, expiry_year, cvv, cardholder_name);
            bool isCard = bankoper.GetCard(card_number, expiry_month, expiry_year, cvv, cardholder_name);
            if(!isCard)
            {
                return Content(HttpStatusCode.OK, "Card not Found");
            }         
            bool isEnough = bankoper.CheckLimit(order.amount_kop, card_number);
            if (!isEnough)
            {
                return Ok("Not Enough money   " + bankoper.GetLimit(card_number));
            }
            try
            {
                bankoper.PayBank(card_number, order);
            }
            catch
            {
                return Ok("Bank Error");
            }

            return Ok("success");
        }

        public IHttpActionResult GetStatus (int id)
        {
            var order = orderoper.FindOrder(id);
           if(order == null)
            {
                return NotFound();
            }
            else return Ok(order.status);
        }

        public IHttpActionResult GetRefund(int id)
        {
            var order = orderoper.FindOrder(id);
            if(order == null)
            {
                return Ok("Order not Found");
            }
            try
            {
                Card card = bankoper.GetCard(order.card_number);
                card.limit += (double)order.amount_kop / 100;
                order.status = "Returned";
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                return Ok("Bank Error");
            }
            return Ok("Success");
        }
              
        public IHttpActionResult GetCardLimit()
        {
            return Ok(bankoper.GetLimit("2222"));
        }

        /*public void PayBank(string number, Order order)
        {
            var card = db.Cards.Find(number);
            if (card.limit >= 0)
                {
                    card.limit -= (double)order.amount_kop / 100;
                    return;
                }          
        }*/
    }
}
