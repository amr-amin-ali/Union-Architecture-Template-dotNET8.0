namespace Elearning.Contracts.Common
{
    using System.Text.Json.Serialization;

    using Elearning.Contracts.Repositories;

    public class BaseModel<T> : BaseModel
    {
        public BaseModel() => Error = new Error();

        public bool HasError => Error.Errors.Any() || Error.Exception != null;

        public T Data { get; set; }
    }

    public class BaseModel
    {
        public BaseModel() => Error = new Error();
        [JsonIgnore]
        public IRepositoryWrapper RepoWrapper { get; set; }
        public Error Error { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
