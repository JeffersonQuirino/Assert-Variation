using AssetVariation.Infra.Dto;
using AssetVariation.Infra.ExternalServices.FinanceYahooChart.Dto;
using System.Threading.Tasks;

namespace AssetVariation.Infra.ExternalServices.FinanceYahooChart.Interfaces
{
    public interface IFinanceYahooChart
    {
        Task<ResponseDto<ResultChartDto>> GetAssetVariation();
        void SetInterval(string interval);
        void SetRange(string range);
        void SetIndex(string index);
    }
}