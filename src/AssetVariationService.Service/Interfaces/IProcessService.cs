using AssetVariation.Infra.Dto;
using System.Threading.Tasks;

namespace AssetVariation.Service.Interfaces
{
    public interface IProcessService
    {
        Task<ResponseDto>ProcessData();

        Task<ResponseDto<AssetDto>> GetAssetVariation();
    }
}