namespace Elearning.Repositories
{
    using AutoMapper;

    using Elearning.Contracts.Repositories;
    using Elearning.Entittes.DbContexts;
    using Elearning.Repositiores;

    using Microsoft.Extensions.Configuration;

    public class RepositoryWrapper : IRepositoryWrapper, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly ElearningContext _elearningContext;
        private IMapper _mapper;
        private ICustomLoggerRepository _cusomLoggerRepository;
        private IOTPDataRepository _otpDataRepository;

        #region  identity

        #endregion
        public RepositoryWrapper(ElearningContext taskContext, IMapper mapper, IConfiguration configuration)
        {
            _elearningContext = taskContext;
            _mapper = mapper;
            _configuration = configuration;

        }

        public ICustomLoggerRepository CustomLoggerRepository => new CustomLoggerRepository(_configuration);
        public ElearningContext Context => _elearningContext;

        public IOTPDataRepository OTPDataRepository
        {
            get
            {
                _otpDataRepository ??= new OTPDataRepository(_elearningContext, _mapper);
                return _otpDataRepository;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _elearningContext.SaveChangesAsync();
        }
        public int Save()
        {
            return _elearningContext.SaveChanges();
        }
        public void Dispose()
        {
            _elearningContext.Dispose();
        }
    }
}
