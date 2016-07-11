using BankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAPITestProject.Models
{
    class FakeContext
    {
        public List<Order> Orders = new List<Order>(); //{ get; set; }
        public List<Card> Cards = new List<Card>();//{ get; set; }
    }
}
