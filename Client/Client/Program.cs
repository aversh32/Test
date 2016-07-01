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
                    // If you reach this point, an exception has been caught.
                    Console.WriteLine("A WebException has been caught.");
                    // Write out the WebException message.
                    Console.WriteLine(webExcp.ToString());
                    // Get the WebException status code.
                    WebExceptionStatus status = webExcp.Status;
                    // If status is WebExceptionStatus.ProtocolError, 
                    //   there has been a protocol error and a WebResponse 
                    //   should exist. Display the protocol error.
                    if (status == WebExceptionStatus.ProtocolError)
                    {
                        Console.Write("The server returned protocol error ");
                        // Get HttpWebResponse so that you can check the HTTP status code.
                        HttpWebResponse httpResponse = (HttpWebResponse)webExcp.Response;
                        Console.WriteLine((int)httpResponse.StatusCode + " - "
                           + httpResponse.StatusCode);
                    }
                }
                    
              //  Console.WriteLine("result   " + result);
                
            }
        }

        static void Post()
        {
            HttpWebRequest r =
       WebRequest.Create("http://localhost:58453/api/order/PostPay/1")
       as HttpWebRequest;

            DateTime creationDate = DateTime.Now;
            // Convert the date to JSON format.
            long ticks = (creationDate.ToUniversalTime().Ticks -
          (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks) / 10000;
            Int32 stateProvinceId = 79;
            Guid rowGuid = Guid.NewGuid();

            // __metadata is only required if inheritance is used.
            string requestPayload = "{__metadata:{Uri:'/Address/', " +
                "Type:'AdventureWorksModel.Address'}, " +
                "AddressLine1:'703 NW 170th St.', " +
                "City:'Kirkland', StateProvinceID:" +
                stateProvinceId.ToString() +
                ", PostalCode:'98021', rowguid:'" +
                rowGuid.ToString() +
                "', ModifiedDate:'\\/Date(" + ticks + ")\\/'}";

            r.Method = "POST";
            UTF8Encoding encoding = new UTF8Encoding();
            r.ContentLength = encoding.GetByteCount(requestPayload);
            r.Credentials = CredentialCache.DefaultCredentials;
            r.Accept = "application/json";
            r.ContentType = "application/json";

            //Write the payload to the request body.
            using (Stream requestStream = r.GetRequestStream())
            {
                requestStream.Write(encoding.GetBytes(requestPayload), 0,
                    encoding.GetByteCount(requestPayload));
            }
            HttpWebResponse response = (HttpWebResponse)r.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            StringBuilder output = new StringBuilder();
            output.Append(reader.ReadToEnd());
            Console.WriteLine("Answer:   "+output);
            response.Close();
            Console.WriteLine(requestPayload);
            Console.WriteLine(r.RequestUri);

        }

        void tp()
        {
            string url = "http://localhost:58453/api/order/PostPay/1";
             Encoding enc8 = Encoding.UTF8;
            using (var webClient = new WebClient())
            {
                // Создаём коллекцию параметров
                var pars = new NameValueCollection();
                // Добавляем необходимые параметры в виде пар ключ, значение
                pars.Add("format", "json");
                // Посылаем параметры на сервер
                // Может быть ответ в виде массива байт
                var response = webClient.UploadValues(url, pars);
                Console.WriteLine(pars.Count);
                Console.WriteLine("answer");
                Console.WriteLine(enc8.GetString(response));           
            }
        }

        static void tp2()
        {
            string ProxyString = "";
            string URI = @"http://localhost:58453/api/order/PostPay/1"; ;
            string Parameters = "proba=sss; ppp=www";

            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            //req.Proxy = new System.Net.WebProxy(ProxyString, true); 
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Parameters);
            req.ContentLength = bytes.Length;
            System.IO.Stream os = req.GetRequestStream(); // создаем поток 
            os.Write(bytes, 0, bytes.Length); // отправляем в сокет 
            os.Close();
            Console.WriteLine(req.RequestUri.Query);
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null)
            {
                Console.WriteLine("Что-то ответ пустой");
                return;
            }
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            Console.WriteLine(sr.ReadToEnd().Trim());
        }

        public static HttpWebResponse PostMethod(string postedData, string postUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(postUrl);
            request.Method = "POST";
            request.Credentials = CredentialCache.DefaultCredentials;
            UTF8Encoding encoding = new UTF8Encoding();
            var bytes = encoding.GetBytes(postedData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;
            using (var newStream = request.GetRequestStream())
            {
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();
            }
            return (HttpWebResponse)request.GetResponse();
        }

        static void Req()
        {
            Console.Write("\nPlease enter the URI to post data to : ");
            string uriString = "http://localhost:58453/api/order/PostPay/1";
            // Create a new WebClient instance.
            WebClient myWebClient = new WebClient();
            // Create a new NameValueCollection instance to hold some custom parameters to be posted to the URL.
            NameValueCollection myNameValueCollection = new NameValueCollection();
            Console.WriteLine("Please enter the following parameters to be posted to the URL");
            Console.Write("Name:");
            string name = Console.ReadLine();
          /*  Console.Write("Age:");
            string age = Console.ReadLine();
            Console.Write("Address:");
            string address = Console.ReadLine();*/
           // Add necessary parameter/value pairs to the name/value container.
            myNameValueCollection.Add("Name", name);
           // myNameValueCollection.Add("Address", address);
           // myNameValueCollection.Add("Age", age);
            Console.WriteLine("\nUploading to {0} ...", uriString);           
            // 'The Upload(String,NameValueCollection)' implicitly method sets HTTP POST as the request method.            
            byte[] responseArray = myWebClient.UploadValues(uriString, myNameValueCollection);
            Console.WriteLine(myWebClient.QueryString.Count);
            // Decode and display the response.
            Console.WriteLine("\nResponse received was :\n{0}", Encoding.UTF8.GetString(responseArray));
        }

        static void FromB()
        {
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var data = "=Short test...";
                var result = client.UploadString("http://localhost:58453/api/order/PostPay/1", "POST", data);
                Console.WriteLine("result   "+result);
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
                new Card { card_number = "2222",  expiry_month="12", expiry_year="1233", cvv="123", cardholder_name="wdfsfg" },
                new Card { card_number = "1234",  expiry_month="11", expiry_year="1111", cvv="567", cardholder_name="Andrey"},
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
            Pay(1, cards[0].card_number, cards[0].expiry_month, cards[0].expiry_year, cards[0].cvv, cards[0].cardholder_name);
            Pay(2, cards[1].card_number, cards[1].expiry_month, cards[1].expiry_year, cards[1].cvv, cards[1].cardholder_name);
            Pay(3, cards[0].card_number, cards[0].expiry_month, cards[0].expiry_year, cards[0].cvv, cards[0].cardholder_name);
         //   GetLimit("2222");
        }

    }
}
