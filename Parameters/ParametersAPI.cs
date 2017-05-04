using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.IO;

namespace NewCadeirinhaIoT.Parameters
{
    public class ParametersAPI
    {
        private static string url = @"http://192.27.1.150:5000/api/cadeirinha/";

        public async Task<string> GetParametersAsync(string popid)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetAsync(url+popid).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                {
                    return await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false);
                }
            }
            return null;
        }

        public static string Get(string popid )
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
                catch (WebException e)
                {
                    Debug.WriteLine(e.Message);                    
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
                return "Not Ok";
            }
        }


        
    }
}
