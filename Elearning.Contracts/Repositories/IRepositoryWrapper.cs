namespace Elearning.Contracts.Repositories
{
    using Elearning.Entittes.DbContexts;

    public interface IRepositoryWrapper
    {
        ElearningContext Context { get; }

        ICustomLoggerRepository CustomLoggerRepository { get; }
        IOTPDataRepository OTPDataRepository { get; }
    }
}