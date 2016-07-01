using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BankAPI.Models
{
    
    public class Order
    {
        [Key]
        public int order_id { get; set; }   
        /*public string card_number { get; set; }
        public string expiry_month { get; set; }
        public string expiry_year { get; set; }
        public string cvv { get; set; }
        public string cardholder_name { get; set; }*/
        public int amount_kop { get; set; }
        public string status { get; set; }
        public string card_number { get; set; }
    }

    public class Card
    {

        [Key]
        public string card_number { get; set; }
        public string expiry_month { get; set; }
        public string expiry_year { get; set; }
        public string cvv { get; set; }
        public string cardholder_name { get; set; }
        public double limit { get; set; }        
    }

    public class Test
    {
        [Key]
        public int id { get; set; }
        public int value { get; set; }
    }
}