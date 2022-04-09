using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace Provider
{
    public class MagaluProvider : IMagaluProvider
    {
        private readonly IConfiguration _configuration;

        public MagaluProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ProductEntity> GetAsync(Guid id)
        {
            try
            {
                string _uri = this._configuration.GetSection("Providers")["LuizaLabs"];
                HttpClient client = new();

                string url = string.Concat(_uri, id, "/");
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    dynamic result = response.Content.ReadAsStringAsync().Result;
                    dynamic json = JsonConvert.DeserializeObject<ProductEntity>(result);                   
                    return await Task.FromResult<ProductEntity>(json);
                }

                client.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
}
