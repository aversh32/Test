using BankAPI.Models.Repositiry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankAPI.Models
{
    public class OrderOperations
    {
       // OrderContext db = new OrderContext();
        private Repository repo = new Repository();
        public Order FindOrder(int id)
        {
            var order = repo.GetOrder(id);
            if (order == null)
                throw new Exception("Order not found");
            
            return order;
        }
    }
}