namespace Elearning.Repositiores
{
    using AutoMapper;

    using Microsoft.EntityFrameworkCore;

    using Elearning.Contracts.Repositories;
    using Elearning.Entittes.Models;
    using Elearning.Repositories;
    using Elearning.Shared.DTOs;
    using Elearning.Entittes.DbContexts;

    public class OTPDataRepository : Repository<OTPData>, IOTPDataRepository
    {
        private readonly IMapper _mapper;
        public OTPDataRepository(ElearningContext elearningContext, IMapper mapper) : base(elearningContext)
        {
            ElearningContext = elearningContext;
            _mapper = mapper;
        }

        public async Task<string> AddOTPData(OTPDataDTO otpData)
        {
            try
            {
                ElearningContext = new ElearningContext();
                var otpDataEntity = _mapper.Map<OTPData>(otpData);
                await ElearningContext.OTPDatas.AddAsync(otpDataEntity);
                await ElearningContext.SaveChangesAsync();
                return "done";
            }
            catch (Exception)
            {
                await ElearningContext.DisposeAsync();
                throw;
            }
        }

        public async Task<OTPData?> GetOTPData(long id)
        {
            try
            {
                return await ElearningContext.OTPDatas.FirstOrDefaultAsync(i => i.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<OTPDataDTO>> GetAllOTPData()
        {
            try
            {
                var SubTasks = await ElearningContext.OTPDatas.ToListAsync();
                var Data = _mapper.Map<IEnumerable<OTPDataDTO>>(SubTasks);
                return Data.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OTPData?> GetOTPDataByUserID(string UserId)
        {
            try
            {
                return await ElearningContext.OTPDatas.FirstOrDefaultAsync(i => i.UserId == UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
