using Geolocation.Infrastructure.DTO;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Services
{    
    public interface IService
    {
        Task<GeolocationResponseDTO> GetDataFromRemoteAPI(string paramter);
    }
}
