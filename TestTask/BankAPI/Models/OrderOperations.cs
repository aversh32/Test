using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankAPI.Models
{
    public class OrderOperations
    {
        OrderContext db = new OrderContext();

        public Order FindOrder(int id)
        {
            var order = db.Orders.Find(id);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            else return order;
        }
    }
}