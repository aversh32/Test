using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Card
    {

        public string card_number { get; set; }
        public int limit { get; set; }
        public string expiry_month { get; set; }
        public string expiry_year { get; set; }
        public string cvv { get; set; }
        public string cardholder_name { get; set; }
        
    }

    class Program
    {
        static void GetOrder(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:58453/api/order/GetOrder/"+id);
            request.Method = "GET";
            request.Accept = "application/json";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            StringBuilder output = new StringBuilder();
            output.Append(reader.ReadToEnd());
            Console.Write(output);
            response.Close();
        }

        static void GetStatus(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:58453/api/order/GetStatus/" + id);
            request.Method = "GET";
            request.Accept = "application/json";
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                StringBuilder output = new StringBuilder();
                output.Append(reader.ReadToEnd());
                Console.Write(output);
                response.Close();
            }
            catch(System.Net.WebException)
            {
                Console.WriteLine("Status Not Found");
            }
            
        }

        static void Pay(int id, string card_number, string expiry_month, string expiry_year, string cvv, string cardholder_name)
        {

            string data = "=" + card_number+" "+expiry_month; 
                //+ "&expiry_month=" + expiry_month.ToString() + "&expiry_year=" + expiry_year.ToString() + "&cvv=" + cvv.ToString() + "&cardholder_name=" + cardholder_name.ToString();
            /*             string testpost = "{test:'abc'}";
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:58453/api/order/PostPay/" + id);
                        request.Method = "POST";
                        UTF8Encoding encoding = new UTF8Encoding();
                        request.Accept = "application/json";
                        request.ContentLength = encoding.GetByteCount(testpost);
                        request.Credentials = CredentialCache.DefaultCredentials;
                        request.ContentType = "application/json";
                        using (Stream writer = request.GetRequestStream())
                        {
                            writer.Write(encoding.GetBytes(testpost), 0,
                         encoding.GetByteCount(testpost));
                        }
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        StringBuilder output = new StringBuilder();
                        output.Append(reader.ReadToEnd());
                        Console.WriteLine("output    "+output);
                        response.Close();
                        Console.WriteLine(testpost);
                        Console.WriteLine(request.RequestUri);*/
          //  Console.WriteLine(data);
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                NameValueCollection myNameValueCollection = new NameValueCollection();
                myNameValueCollection.Add("c_number", card_number);
                myNameValueCollection.Add("exp_month", expiry_month);
                myNameValueCollection.Add("exp_year", expiry_year);
                myNameValueCollection.Add("cvv", cvv);
                myNameValueCollection.Add("cardholder", cardholder_name);
                try
                {
                    var result = client.UploadValues("http://localhost:58453/api/order/PostPay/" + id, "POST", myNameValueCollection);
                    Console.WriteLine("\nResponse received was :\n{0}", Encoding.ASCII.GetString(result));
                }
                catch (System.Net.WebException webExcp)
                {
                    Console.WriteLine("A WebException has been caught.");
                    Console.WriteLine(webExcp.Message.ToString());
                    WebExceptionStatus status = webExcp.Status;                  
                }
                    
              //  Console.WriteLine("result   " + result);
                
            }
        }

        static void GetLimit(string number)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:58453/api/order/GetCardLimit/" + number);
            request.Method = "GET";
            request.Accept = "application/json";
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                StringBuilder output = new StringBuilder();
                output.Append(reader.ReadToEnd());
                Console.Write("Limit=   "+output);
                response.Close();
            }
            catch (System.Net.WebException)
            {
                Console.WriteLine("Not Found");
            }
        }

        static void Main(string[] args)
        {
             Card[] cards = new Card[]
            {
                new Card { card_number = "2222",  expiry_month="12", expiry_year="2017", cvv="123", cardholder_name="wdfsfg" },
                new Card { card_number = "1234",  expiry_month="11", expiry_year="2018", cvv="567", cardholder_name="Andrey"},
                new Card { card_number = "9999",  expiry_month="11", expiry_year="2018", cvv="567", cardholder_name="Andrey"},
            };
             GetOrder(1);
            Console.Write("\n");
             GetOrder(2);
            Console.Write("\n");
            GetStatus(1);
            Console.Write("\n");
            GetStatus(2);
            Console.Write("\n");
            GetStatus(6);
            Console.Write("\n");
          //  Pay(1, cards[0].card_number, cards[0].expiry_month, cards[0].expiry_year, cards[0].cvv, cards[0].cardholder_name);
          //  Pay(2, cards[1].card_number, cards[1].expiry_month, cards[1].expiry_year, cards[1].cvv, cards[1].cardholder_name);
           // Pay(3, cards[0].card_number, cards[0].expiry_month, cards[0].expiry_year, cards[0].cvv, cards[0].cardholder_name);
            Pay(3, "7787", cards[2].expiry_month, cards[2].expiry_year, cards[2].cvv, cards[2].cardholder_name);
            //   GetLimit("2222");
        }

    }
}
