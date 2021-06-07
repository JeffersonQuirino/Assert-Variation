
using AssetVariation.Infra.Dto;
using AssetVariation.Infra.ExternalServices.FinanceYahooChart.Dto;
using AssetVariation.Infra.ExternalServices.FinanceYahooChart.Interfaces;
using AssetVariation.Infra.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AssetVariation.Infra.ExternalServices.FinanceYahooChart.Implementation
{
    public class FinanceYahooChart : IFinanceYahooChart
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly AppSettings appSettings;

        public FinanceYahooChart(IHttpClientFactory clientFactory,
                                 IOptions<AppSettings> appSettings)
        {
            this.clientFactory = clientFactory;
            this.appSettings = appSettings.Value;
        }

        public string Interval { get; private set; }
        public string Range { get; private set; }
        public string Index { get; private set; }

        public void SetInterval(string interval)
        {
            this.Interval = interval;          
        }

        public void SetRange(string range)
        {
            this.Range = range;
        }

        public void SetIndex(string index)
        {
            this.Index = index;
        }

        public async Task<ResponseDto<ResultChartDto>> GetAssetVariation()
        {
            ResponseDto<ResultChartDto> responseResult = new();

            try
            {
                if (!ValidateRequest(responseResult))
                    return responseResult;

                var client = clientFactory.CreateClient();
                client.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"{appSettings.FinanceYahoo.Url}/{this.Index}?interval={this.Interval}&range={this.Range}");

                if (response.IsSuccessStatusCode)
                {
                    responseResult.Content = JsonConvert.DeserializeObject<ResultChartDto>(await response.Content.ReadAsStringAsync());
                    responseResult.Result = response.IsSuccessStatusCode;
                }
                else
                {
                    responseResult.Message = $"StatusCode: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                responseResult.Message = $"{ex.Message} Request: {this}";
            }

            return responseResult;
        }

        public override string ToString()
        {
            return $"Index: {this.Index ?? string.Empty} Interval: {this.Interval ?? string.Empty} Range: {this.Range ?? string.Empty} Url: {appSettings?.FinanceYahoo?.Url ?? string.Empty}";
        }

        private bool ValidateRequest(ResponseDto responseResult)
        {
            if (appSettings == null ||
               appSettings.FinanceYahoo == null ||
               string.IsNullOrWhiteSpace(appSettings.FinanceYahoo.Url) ||
               string.IsNullOrWhiteSpace(this.Index) ||
               string.IsNullOrWhiteSpace(this.Interval) ||
               string.IsNullOrWhiteSpace(this.Range)
               )
            {
                responseResult.Message = $"{ Properties.Resources.Invalid_Attributes_Request_Finance_Yahoo_Chart} Request: {this}";
                responseResult.Result = false;
                return false;
            }

            return true;
        }      
    }
}