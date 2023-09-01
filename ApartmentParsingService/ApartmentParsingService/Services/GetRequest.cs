using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentParsingService.Services
{
    public class GetRequest
    {
        HttpWebRequest Request;
        string Address;
        public string Respose { get; set; }
        public GetRequest(string address)
        {
            Address = address;
        }

        public void Run()
        {
            try
            {
                Request = (HttpWebRequest)WebRequest.Create(Address);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Request.Method = "Get";
            try
            {
                HttpWebResponse response = (HttpWebResponse)Request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream != null) Respose = new StreamReader(stream).ReadToEnd();
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка get-запроса!");
            }
        }
    }
}
