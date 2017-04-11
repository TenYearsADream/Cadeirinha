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
        private string url;
        private string server = "m9249";
        public static HttpClient client = new HttpClient();      
        
        public static IEnumerable<string> GetParameterAsync(string path)
        {
            client.BaseAddress = new Uri("http://10.33.22.56/lts/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            IEnumerable<string> popid = new List<string>();
            HttpResponseMessage response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                popid = response.Content.ReadAsAsync<IEnumerable<string>>().Result;
            }
            
            return popid;            
        }


        //public IEnumerable<string> Get()
        //{

        //}
    }
}
