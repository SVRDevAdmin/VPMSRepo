using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Transactions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VPMS.Interface.API.VCheck.RequestMessage;
using System.Diagnostics;
using VPMS.Interface.API.VCheck.ResponseMessage;

namespace VPMS.Interface.API
{
    public class VCheckAPI
    {
        public static IConfiguration iConfig;

        public VCheckAPI()
        {
            var sBuilder = new ConfigurationBuilder();
            sBuilder.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            iConfig = sBuilder.Build();
        }

        /// <summary>
        /// Get Test List master
        /// </summary>
        /// <param name="sRequest"></param>
        /// <returns></returns>
        public GetTestListResponse GetTestList(GetTestListRequest sRequest)
        {
            String? sRequestURL = iConfig.GetSection("VCheckAPI:GetTestList").Value;

            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage resp = client.PostAsJsonAsync(sRequestURL, sRequest).Result;
                    if (resp.IsSuccessStatusCode)
                    {
                        String strResult = resp.Content.ReadAsStringAsync().Result;
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<VCheck.ResponseMessage.GetTestListResponse>(strResult);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public GetLocationListResponse GetLocationList(GetLocationListRequest sRequest)
        {
            String? sRequestURL = iConfig.GetSection("VCheckAPI:GetLocationList").Value;

            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage resp = client.PostAsJsonAsync(sRequestURL, sRequest).Result;
                    if (resp.IsSuccessStatusCode)
                    {
                        String strResult = resp.Content.ReadAsStringAsync().Result;
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<VCheck.ResponseMessage.GetLocationListResponse>(strResult);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Submit scheduled Test to VCheckViewer
        /// </summary>
        /// <param name="sRequest"></param>
        /// <returns></returns>
        public CreateScheduledTestResponse CreateScheduledTest(CreateScheduledTestRequest sRequest)
        {
            String? sRequestURL = iConfig.GetSection("VCheckAPI:CreateScheduledTest").Value;

            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage resp = client.PostAsJsonAsync(sRequestURL, sRequest).Result;
                    if (resp.IsSuccessStatusCode)
                    {
                        String strResult = resp.Content.ReadAsStringAsync().Result;
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<VCheck.ResponseMessage.CreateScheduledTestResponse>(strResult);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
