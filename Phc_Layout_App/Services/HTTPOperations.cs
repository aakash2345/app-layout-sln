using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Phc_Layout.Services
{
    public class HTTPOperations
    {
        readonly IConfiguration _config;
        readonly string URI = string.Empty;
        public HTTPOperations(IConfiguration configuration)
        {
            this._config = configuration; 
            this.URI = this._config["SupplierApiUrl:Url"];
        }

        #region 'Public Method'

        public dynamic GetRequest(String methodName, String id = null)
        {
            String jsonResponse = String.Empty;
            if (!String.IsNullOrWhiteSpace(id))
            {
                methodName = String.Format("{0}/{1}", methodName, id);
            }

            var client = this.GetRestClient(methodName);
            var request = new RestRequest(Method.GET);
            var response = new RestResponse();
            //client.AddDefaultHeader("Authorization", AuthorizationToken);

            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();

            if (!String.IsNullOrWhiteSpace(response.Content))
            {
                return JsonConvert.DeserializeObject<dynamic>(response.Content);
            }
            return jsonResponse;
        }

        public dynamic PostRequest(String methodName, String JsonRequestData)
        {
            String jsonResponse = String.Empty;

            RestClient restClient = this.GetRestClient(methodName);
            var request = new RestRequest(Method.POST);
            request.AddParameter("text/json", JsonRequestData, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            var response = new RestResponse();

            Task.Run(async () =>
            {
                response = await this.GetResponseContentAsync(restClient, request) as RestResponse;
            }).Wait();

            if (!String.IsNullOrWhiteSpace(response.Content))
            {
                return JsonConvert.DeserializeObject<dynamic>(response.Content);
            }
            return jsonResponse;

        }

        #endregion

        #region 'Private Method'

        RestClient GetRestClient(String methodName)
        {
            RestClient restClient = new RestClient(URI + "/" + methodName);
            


            return restClient;
        }

        Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response =>
            {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }


        #endregion

    }
}
