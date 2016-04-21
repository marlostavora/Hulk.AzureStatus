using Hulk.Repository.Helpers.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Hulk.Repository.Helpers
{
    public static class AzureStatusEngine
    {
        private const string BASE_URL = "https://www.windowsazurestatus.com/odata/ServiceCurrentIncidents?api-version=1.0";

        private static AzureStatus MakeRequest(string requestUrl)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(AzureStatus));
                    object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                    AzureStatus jsonResponse = objResponse as AzureStatus;
                    return jsonResponse;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static AzureStatus GetAzureStatus()
        {
            return MakeRequest(BASE_URL);
        }
    }
}
