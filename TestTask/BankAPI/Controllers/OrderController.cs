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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace BankAPI.Controllers
{
    public class OrderController : ApiController
    {
        BankOperations bankoper = new BankOperations();
        OrderOperations orderoper = new OrderOperations();

         [System.Web.Http.HttpPost]
        public IHttpActionResult PostPay(int id, [FromBody] Card card )
        {
            Result result = new Result();

            try
            {
                bankoper.Payment(id, card);
            }
            catch(CardValidationException e)
            {
                result.Code = "400";
                result.Definition = e.Message;
                APILog.WriteToLogFile(Request.RequestUri.ToString(), e.Message);
                return Content(HttpStatusCode.BadRequest, result);
            }
            catch(OrderErrorException e)
            {
                result.Code = "400";
                result.Definition = e.Message;
                APILog.WriteToLogFile(Request.RequestUri.ToString(), e.Message);
                return Content(HttpStatusCode.BadRequest, result);
            }
            catch(BankErrorException e)
            {
                result.Code = "500";
                result.Definition = e.Message;
                APILog.WriteToLogFile(Request.RequestUri.ToString(), e.Message);
                return Content(HttpStatusCode.BadRequest, result);
            }
            catch(Exception e)
            {
                result.Code = "404";
                result.Definition = e.Message;
                APILog.WriteToLogFile(Request.RequestUri.ToString(), e.Message);
                return Content(HttpStatusCode.BadRequest, result);
            }

            result.Code = "200";
            result.Definition = "Success";
            APILog.WriteToLogFile(Request.RequestUri.ToString(), "Success");
            return Content(HttpStatusCode.OK, result);
        }

        public  IHttpActionResult GetStatus (int id)
        {
            Order order;
            Result result = new Result();
                  
            try
            {
                order = orderoper.FindOrder(id);
            }
            catch(Exception e)
            {
                result.Code = "404";
                result.Definition = e.Message;
                APILog.WriteToLogFile(Request.RequestUri.ToString(), e.Message);
                return Content(HttpStatusCode.NotFound, result);
            }

            result.Code = "200";
            result.Definition = "Success";
            result.Data = order.status;
            APILog.WriteToLogFile(Request.RequestUri.ToString(), "Success");
            return Ok(result);
        }

        public IHttpActionResult GetRefund(int id)
        {
            Result result = new Result();
            try
            {
                bankoper.OrderRefund(id);
            }
            catch(Exception e)
            {
                result.Code = "500: InternalServerError";
                result.Definition = e.Message;
                APILog.WriteToLogFile(Request.RequestUri.ToString(), e.Message);
                return Content(HttpStatusCode.InternalServerError,result);
            }
            result.Code = "200: OK";
            result.Definition = "Success";
            APILog.WriteToLogFile(Request.RequestUri.ToString(), "Success");
            return Ok(result);
        }
    }
}
