using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebShop.Areas
{
	public class Base : Controller
	{

        private HttpClient _HttpClient;
        public Base()
		{
            _HttpClient = new HttpClient();
            _HttpClient.Timeout = TimeSpan.FromSeconds(Convert.ToInt32(240));  
            _HttpClient.BaseAddress = new Uri("http://localhost:1100/api/"); // Replace with your API base URL

        }

        public async Task<Treturn> wsPost<Treturn, Tmodel>(string requestUri, Tmodel value)
		{


            try
            {
                var response = await _HttpClient.PostAsJsonAsync<Tmodel>(requestUri, value);
                var msg = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Treturn>(msg);

            }
            catch (AggregateException ex)
            {
                //For reference: json object string lentgh is ~269700 when posting to method DLSPTaddImagesLPApp
                if (ex.InnerExceptions.Any(x => x is TaskCanceledException))
                {
                    return default(Treturn);
                }

                throw ex;
            }
            catch (Exception ex) { throw ex; }
            
        }


     




    }
}
