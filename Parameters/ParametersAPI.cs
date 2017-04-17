using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace NewCadeirinhaIoT.Parameters
{
    public abstract class ParametersAPI
    {
        private static string url = "http://192.27.1.150:5000/api/cadeirinha/";
        private string server = "m9249";


        public static string Get(string popid)
        {

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync(url + popid).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content.ReadAsStringAsync().Result;
                    }
                }               
                catch(WebException e)
                {
                    Debug.WriteLine(e.Message);                    
                }
                return "Not Ok";
            }
        }


        
    }
}
