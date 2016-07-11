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
        public int amount_kop { get; set; }
        public string status { get; set; }
        public string card_number { get; set; }
    }

    public class Card
    {
        [Key]
        [RegularExpression(@"[0-9]{1,20}")] 
        public string card_number { get; set; }
        [RegularExpression(@"0[1-9]|1[012]")]
        public string expiry_month { get; set; }
        [RegularExpression(@"(20)\d\d")]
        public string expiry_year { get; set; }
        [Required][RegularExpression(@"\d{3}")][StringLength(3, MinimumLength = 3, ErrorMessage = "Wrong length cvv")]
        public string cvv { get; set; }
        [RegularExpression(@"[a-zA-Z\s]{5,40}")]
        public string cardholder_name { get; set; }
        public double limit { get; set; }        
    }
}