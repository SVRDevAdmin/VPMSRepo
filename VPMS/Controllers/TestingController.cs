using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;

namespace VPMSWeb.Controllers
{
	public class TestingController : Controller
	{
        private readonly string instagramBaseAPIUrl = "https://graph.instagram.com/";
        private readonly string userToken = "IGAANP0v6ItJdBZAFBLbDQ0elM5cGhZAUEtBak1RRmRtMDNPNXJkRkQtc3ZAhMDU3Q0EtRHBNLWZAWRUdIb3dNT0lLV1V0VFo4cFZAOT0UwLTZAxNUVwLUVHMkIwNHRicnkxQlN1dDZADdG5LNGpPeFBrMnRPMUp4VGlYallfMF8yb2h0cwZDZD";

        public IActionResult Index()
        {
            var invoiceNoTemp = "V001V202412160001";
            var test = "V" + invoiceNoTemp.Substring(1).Replace("V", "R").ToString();

            using (var client = new HttpClient())
            {
                try
                {
                    string queryUrl = $"me/media?fields=id,username,timestamp,caption,media_url,media_type,permalink&access_token={userToken}";
                    string fullUrl = $"{this.instagramBaseAPIUrl}{queryUrl}";

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetStringAsync(new Uri(fullUrl)).Result;

                    if (!string.IsNullOrEmpty(response))
                    {
                        var result = JsonConvert.DeserializeObject<InstagramMediaContentResult>(response);
                        ViewData["Content"] = result.Data;
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return View();
		}

        public IEnumerable<InstagramMediaContent> GetInstagramContents(string userToken)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string queryUrl = $"me/media?fields=id,username,timestamp,caption,media_url,media_type,permalink&access_token={userToken}";
                    string fullUrl = $"{this.instagramBaseAPIUrl}{queryUrl}";

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = client.GetStringAsync(new Uri(fullUrl)).Result;

                    if (!string.IsNullOrEmpty(response))
                    {
                        var result = JsonConvert.DeserializeObject<InstagramMediaContentResult>(response);
                        return result.Data;
                    }

                    return new List<InstagramMediaContent>();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }

    public class InstagramMediaContent
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("media_type")]
        public string Media_Type { get; set; }

        [JsonProperty("media_url")]
        public string Media_Url { get; set; }

        [JsonProperty("permalink")]
        public string Permalink { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("timestamp")]
        public DateTime TimeStamp { get; set; }
    }

    public class InstagramMediaContentResult
    {
        [JsonProperty("data")]
        public IEnumerable<InstagramMediaContent> Data { get; set; }
    }
}
