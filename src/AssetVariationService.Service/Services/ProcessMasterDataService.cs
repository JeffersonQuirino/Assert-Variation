using AssetVariation.Infra.Dto;
using AssetVariation.Infra.ExternalServices.FinanceYahooChart.Interfaces;
using AssetVariation.Service.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AssetVariation.Service.Services
{
    public class ProcessMasterDataService : IProcessService
    {
        const string asseDefault = "PETR4.SA";
        const string rangeDefault = "30d";
        const string intervalDefault = "1d";

        private readonly IFinanceYahooChart financeYahooChart;
        private readonly IAssetService assetService;
        private readonly ITraddingFloorService traddingFloorService;

        public ProcessMasterDataService(IFinanceYahooChart financeYahooChart,
                                 IAssetService assetService,
                                 ITraddingFloorService traddingFloorService)
        {
            this.financeYahooChart = financeYahooChart;
            this.assetService = assetService;
            this.traddingFloorService = traddingFloorService;
        }

        public async Task<ResponseDto<AssetDto>> GetAssetVariation()
        {
            ResponseDto<AssetDto> result = new();

            try
            {
                var assetPETR4 = new AssetDto { Name = asseDefault };
                var asset = await assetService.Select(assetPETR4);

                if(asset?.Any() == true)
                {
                    result.Content = asset.FirstOrDefault();
                    result.Content.TraddingFloors = result.Content.TraddingFloors.OrderBy(o => o.OperationDate).ToList();
                }
                else
                {
                    result.Message = $"O ativo {asseDefault} não foi processado!";
                }


            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        public async Task<ResponseDto> ProcessData()
        {
            ResponseDto result = new();

            try
            {
                var assetPETR4 = new AssetDto { Name = asseDefault };
                var asset = await assetService.Select(assetPETR4);

                if (asset?.Any() == true)
                {
                    result.Message = $"O ativo {asseDefault} já foi processado!";
                    return result;
                }

                financeYahooChart.SetRange(rangeDefault);
                financeYahooChart.SetIndex(assetPETR4.Name);
                financeYahooChart.SetInterval(intervalDefault);
                var resultFinanceYahooChart = await financeYahooChart.GetAssetVariation();

                if(!resultFinanceYahooChart.Result)
                {
                   return resultFinanceYahooChart;                 
                }

                var responseChart = resultFinanceYahooChart.Content;

                if (responseChart != null &&
                    responseChart.Chart != null &&
                    responseChart.Chart.Result?.Any() == true &&
                    responseChart.Chart.Result.Any(r => r?.Indicators?.Quote.Length > 0 && r.Indicators.Quote.Any(o => o.Open?.Any() == true) && r.Indicators.Quote.Any(q => q?.Open.Any() == true) &&
                    responseChart.Chart.Result.Any(r => r.Timestamp?.Any() == true && r.Timestamp.Length == r.Indicators.Quote[0]?.Open.Length))
                   )
                {
                    foreach (var item in responseChart.Chart.Result)
                    {
                        for (int i = 0; i < item.Indicators.Quote[0].Open.Length; i++)
                        {
                            assetPETR4.TraddingFloors.Add(new TraddingFloorDto
                            {                               
                                Asset = assetPETR4,
                                AssetId = assetPETR4.Id,
                                Value = item.Indicators.Quote[0].Open[i] ?? 0,
                                OperationDate = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(item.Timestamp[i]).ToLocalTime().Date,
                                Variation = (i > 0 ? (((Convert.ToDecimal(item.Indicators.Quote[0].Open[i]) / Convert.ToDecimal(item.Indicators.Quote[0].Open[i - 1])) - 1) * (100)) : 0)

                            });
                        }
                    }
                }

                assetPETR4.TraddingFloors = assetPETR4.TraddingFloors.OrderBy(o => o.OperationDate).ToList();

                var resultSave = await assetService.Add(assetPETR4);

                result.Result = resultSave.TraddingFloors.Count == assetPETR4.TraddingFloors.Count;
            }
            catch (Exception ex)
            {
                result.Message = ex.InnerException?.Message ?? ex.Message ?? ex.StackTrace;
            }

            return result;
        }
    }
}