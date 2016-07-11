using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using static Client.Program;
using System.Runtime.Serialization;
using Classes;

namespace Client
{
    class Program
    {
        public static async void Pay(int id, string card_number, string expiry_month, string expiry_year, string cvv, string cardholder_name)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://localhost:58453");
                var url = String.Format("http://localhost:58453/api/order/PostPay/" + id);
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("card_number", card_number),
                    new KeyValuePair<string, string>("expiry_month", expiry_month),
                    new KeyValuePair<string, string>("expiry_year", expiry_year),
                    new KeyValuePair<string, string>("cvv", cvv),
                    new KeyValuePair<string, string>("cardholder_name", cardholder_name)
                });
               // var result = client.PostAsync(url, content).Result;
                  var response = await client.PostAsync(url, content);
                 var result = response.Content;
                //string resultContent = result.Content.ReadAsStringAsync().Result;
                 string resultContent = await result.ReadAsStringAsync();
                var jss = new JavaScriptSerializer();
                var dict = jss.Deserialize<Dictionary<string, string>>(resultContent);
                var code = dict["Code"];
                var def = dict["Definition"];
                var data = dict["Data"];
                Console.WriteLine(url + "   " + code + "   " + def + " " + data + " " + id);
            }
        }


        static void Main(string[] args)
        {
             Card[] cards = new Card[]
            {
                new Card() { card_number = "2222",  expiry_month="12", expiry_year="2017", cvv="123", cardholder_name="wdfsfg" },
                new Card() { card_number = "1234",  expiry_month="11", expiry_year="2018", cvv="567", cardholder_name="ANDREY"},
                new Card() { card_number = "9999",  expiry_month="01", expiry_year="2018", cvv="532asd", cardholder_name="Andrey"},
            };
            API.GetStatus(1).GetAwaiter().GetResult();
            API.GetStatus(2).Wait();
            API.GetStatus(6).Wait();

            API.Pay(1, cards[0].card_number, cards[0].expiry_month, cards[0].expiry_year, cards[0].cvv, cards[0].cardholder_name);
            API.Pay(3, cards[1].card_number, cards[1].expiry_month, cards[1].expiry_year, cards[1].cvv, cards[1].cardholder_name);
            API.Pay(2, cards[0].card_number, cards[0].expiry_month, cards[0].expiry_year, cards[0].cvv, cards[0].cardholder_name);
            API.Refund(2);
            API.Refund(4);
            API.Pay(3, "7787", cards[2].expiry_month, cards[2].expiry_year, cards[2].cvv, cards[2].cardholder_name);
            Console.WriteLine("End");
        }
    }
}
