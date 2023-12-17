using Elearning.Entittes.Models;
using Elearning.Shared.DTOs;

namespace Elearning.Contracts.Repositories
{
    public interface IOTPDataRepository:IRepository<OTPData>
    {
        Task<List<OTPDataDTO>> GetAllOTPData();
        Task<string> AddOTPData(OTPDataDTO Task);
        Task<OTPData?> GetOTPData(long id);
        Task<OTPData?> GetOTPDataByUserID(string UserId);
    }
}
