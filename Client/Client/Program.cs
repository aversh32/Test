using System;
using System.Collections.Generic;
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
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            StringBuilder output = new StringBuilder();
            output.Append(reader.ReadToEnd());
            Console.Write(output);
            response.Close();
        }

        static void Pay(int id, string card_number, string expiry_month, string expiry_year, string cvv, string cardholder_name)
        {
            string postParameters = "card_number=" + card_number + "&expiry_month=" + expiry_month + "&expiry_year=" + expiry_year + "&cvv=" + expiry_year + "&cardholder_name=" + cardholder_name;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:58453/api/order/PostPay/" + id);
            request.Method = "Post";
            request.Accept = "application/json";
            request.ContentLength = postParameters.Length;
            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(postParameters);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            StringBuilder output = new StringBuilder();
            output.Append(reader.ReadToEnd());
            Console.Write(output);
            response.Close();
            Console.Write(request.RequestUri);
        }

        static void Main(string[] args)
        {
             Card[] cards = new Card[]
            {
                new Card { card_number = "2222", limit = 1000, expiry_month="12", expiry_year="1233", cvv="123", cardholder_name="wdfsfg" },
                new Card { card_number = "1234", limit = 0, expiry_month="11", expiry_year="1111", cvv="567", cardholder_name="Andrey"},
            };
             GetOrder(1);
            Console.Write("\n");
             GetOrder(2);
            Console.Write("\n");
            GetStatus(1);
            Console.Write("\n");
            GetStatus(2);
            Console.Write("\n");
            Pay(1, cards[0].card_number, cards[0].expiry_month, cards[0].expiry_year, cards[0].cvv, cards[0].cardholder_name);

        }

    }
}
