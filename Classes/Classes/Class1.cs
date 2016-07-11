using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Classes
{
    public class Order
    {
        public int order_id { get; set; }
        public int amount_kop { get; set; }
        public string status { get; set; }
        public string card_number { get; set; }
    }

    public class Card
    {
        public string card_number { get; set; }
        public string expiry_month { get; set; }
        public string expiry_year { get; set; }
        public string cvv { get; set; }
        public string cardholder_name { get; set; }
        public double limit { get; set; }
    }

    public class ApiResult
    {
        public string Code { get; set; }
        public string Definition { get; set; }
        public string Data { get; set; }
    }

    public class API
    {
        public static async Task GetStatus(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    ApiResult answer = new ApiResult();
                    var http = new HttpClient();
                    var url = String.Format("http://localhost:58453/api/order/GetStatus/" + id);
                    var response = await http.GetAsync(url);
                    var result = await response.Content.ReadAsStringAsync();
                    // string json = @"{""key1"":""value1"",""key2"":""value2""}";
                    var jss = new JavaScriptSerializer();
                    var dict = jss.Deserialize<Dictionary<string, string>>(result);
                    var code = dict["Code"];
                    var def = dict["Definition"];
                    var data = dict["Data"];
                    answer = JsonConvert.DeserializeObject<ApiResult>(result);
                    Console.WriteLine(url + "   " + code + "   " + def + " " + data + " " + id);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public  static async void Pay(int id, string card_number, string expiry_month, string expiry_year, string cvv, string cardholder_name)
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
                 var result =  client.PostAsync(url, content).Result;
              //  var response = await client.PostAsync(url, content);
               // var result = response.Content;
                 string resultContent = result.Content.ReadAsStringAsync().Result;
               // string resultContent = await result.ReadAsStringAsync();
                var jss = new JavaScriptSerializer();
                var dict = jss.Deserialize<Dictionary<string, string>>(resultContent);
                var code = dict["Code"];
                var def = dict["Definition"];
                var data = dict["Data"];
                Console.WriteLine(url + "   " + code + "   " + def + " " + data + " " + id);
                
            }
        }

        public static async void Refund(int order_id)
        {
            using (var client = new HttpClient())
            {
                var http = new HttpClient();
                var url = String.Format("http://localhost:58453/api/order/GetRefund/" + order_id);
                var response = await http.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                var jss = new JavaScriptSerializer();
                var dict = jss.Deserialize<Dictionary<string, string>>(result);
                var code = dict["Code"];
                var daf = dict["Definition"];
                var data = dict["Data"];
                Console.WriteLine(url + "  " + code + "   " + order_id);
            }
        }
    }


}
