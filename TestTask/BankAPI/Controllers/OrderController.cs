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
 
       // OrderContext db = new OrderContext();
        BankOperations bankoper = new BankOperations();
        OrderOperations orderoper = new OrderOperations();
        
         /* public IEnumerable<Order> GetAllOrders()
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
   */
         [System.Web.Http.HttpPost]
        public IHttpActionResult PostPay(int id, [FromBody] FormDataCollection values )
        {
            APILog.LogWrite(Request.RequestUri.ToString());
            Order order;
            try
            {
                 order = orderoper.FindOrder(id);
            }
            catch(Exception e)
            {
                APILog.LogWrite(e.Message);
                return Content(HttpStatusCode.NotFound, e.Message);
            }
            string card_number = values["c_number"];
            string expiry_month = values["exp_month"];
            string expiry_year = values["exp_year"];
            string cvv = values["cvv"];
            string cardholder_name = values["cardholder"];
            try
            {
                Validaion.TryValidate(card_number, expiry_month, expiry_year, cvv, cardholder_name);
            }
            catch(CardException e)
            {
                APILog.LogWrite(e.Message);
                return Content( HttpStatusCode.BadRequest, e.Message);
            }
            try
            {
                bankoper.GetCard(card_number, expiry_month, expiry_year, cvv, cardholder_name);
            }
            catch(Exception e)
            {
                APILog.LogWrite(e.Message);
                //return Content(HttpStatusCode.NotFound, "Card not Found");
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, e.Message));
            }
            try
            {
                bankoper.CheckLimit(order.amount_kop, card_number);
            }
           catch(Exception e)
            {
                APILog.LogWrite(e.Message);
                return Content(HttpStatusCode.Forbidden, e.Message);
            }
            try
            {
                bankoper.PayBank(card_number, order);
            }
            catch(Exception e)
            {
                APILog.LogWrite(e.Message);
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
            APILog.LogWrite("Success");
            return Ok("success");
        }

        public IHttpActionResult GetStatus (int id)
        {
            APILog.LogWrite(Request.RequestUri.ToString());
            Order order;
            try
            {
                order = orderoper.FindOrder(id);
            }
            catch(Exception e)
            {
                APILog.LogWrite(e.Message);
                return Content(HttpStatusCode.NotFound, e.Message);
            }
            APILog.LogWrite("Success");
            return Ok(order.status);
        }

        public IHttpActionResult GetRefund(int id)
        {
            APILog.LogWrite(Request.RequestUri.ToString());
            try
            {
                bankoper.OrderRefund(id);
            }
            catch(Exception e)
            {
                APILog.LogWrite(e.Message);
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
            APILog.LogWrite("Success");
            return Ok("Success");
        }
    }
}
