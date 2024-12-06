using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class HttpCustomClient
    {
        private static readonly HttpClient client = new()
        {
            BaseAddress = new Uri("https://localhost:7269/")
      
        };


        public async Task<string> post(string endPointUrl, HttpContent body)
        {
            //await client.PostAsJsonAsync(endPointUrl, body);
            using HttpResponseMessage response = await client.PostAsync(endPointUrl, body);
            string responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public async Task<string> put(string endPointUrl, HttpContent body)
        {
            //await client.PostAsJsonAsync(endPointUrl, body);
            using HttpResponseMessage response = await client.PutAsync(endPointUrl, body);
            string responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public async Task<string> get(string endPointUrl)
        {
            using HttpResponseMessage response = await client.GetAsync(endPointUrl);
            string responseString = await response.Content.ReadAsStringAsync();
            // Deserializar el JSON a una lista de objetos BebidaResumen
            return responseString;
        }
    }
}
