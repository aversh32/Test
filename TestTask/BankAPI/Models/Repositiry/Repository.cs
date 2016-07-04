using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankAPI.Models.Repositiry
{
    public class Repository
    {
        private OrderContext context = new OrderContext();

        public IEnumerable<Order> Orders
        {
            get { return context.Orders; }
        }

        public IEnumerable<Card> Cards
        {
            get { return context.Cards; }
        }
    }
}