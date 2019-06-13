using AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using WebClient.Model;

namespace WebClient
{
    public class APIService
    {
        #region Fields
        private HttpClient client;
        private string Uri;
        #endregion

        #region Constructor
        public APIService()
        {
            Uri = "http://localhost:57988/api/payment";
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion


        #region Methods

        public async Task<ResponseDto> GetPaymentAsync()
        {
            var streamTask = client.GetStreamAsync(Uri);
            var serializer = new DataContractJsonSerializer(typeof(ResponseDto));
            var responseDto = serializer.ReadObject(await streamTask) as ResponseDto;
            return responseDto;
        }

        public async Task<ResponseDto> GetPaymentAsync(string uid)
        {
            var streamTask = client.GetStreamAsync(Uri + "/" + uid);
            var serializer = new DataContractJsonSerializer(typeof(ResponseDto));
            var responseDto = serializer.ReadObject(await streamTask) as ResponseDto;
            return responseDto;
        }
           
        public async Task<ResponseDto> MakePaymentAsync(IPayment payment)
        {
            // works after I chnaged the object we send toa plain base class object without decorators
            var dataAsString = JsonConvert.SerializeObject(payment);
            var response = await client.PostAsJsonAsync(Uri, payment);
            var msg = await response.Content.ReadAsStreamAsync();
            var serializer = new DataContractJsonSerializer(typeof(ResponseDto));
            var responseDto = serializer.ReadObject(msg) as ResponseDto;

            return responseDto;
        }

        #endregion
    }
}
