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

    public class Test
    {
        [Key]
        [Required(ErrorMessage = "Идентификатор пользователя не установлен", AllowEmptyStrings = false)]
        [StringLength(3, ErrorMessage = "Too long")]
        public string User { get; set; }
        [Required(ErrorMessage = "Не указано имя пользователя")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Недопустимая длина имени")]
        public string Name { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "Недопустимый возраст")]
        public int Age { get; set; }
    }
}