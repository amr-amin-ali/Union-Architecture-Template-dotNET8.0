using Elearning.Contracts.Common;
using Elearning.Shared.DTOs;

namespace Elearning.Contracts.Services
{
    public interface IOTPDataService
    {
        Task<GenericResponseModel<List<OTPDataDTO>>> GetAllOTPDatas();
        Task<GenericResponseModel<OTPDataDTO>> GetOTPDataByID(long id);

    }
}
