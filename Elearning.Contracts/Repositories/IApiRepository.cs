
namespace Elearning.Contracts.Repositories
{
    public interface IApiRepository<T> where T : class
    {
        Task<T> Get(string url);

        /// <summary>
        /// For creating a new item over a web api using POST
        /// </summary>
        /// <param name="apiUrl">API Url</param>
        /// <param name="postObject">The object to be created</param>
        /// <returns>A Task with created item</returns>
        Task<T> PostRequest(string apiUrl, T postObject);

        /// <summary>
        /// For updating an existing item over a web api using PUT
        /// </summary>
        /// <param name="apiUrl">API Url</param>
        /// <param name="putObject">The object to be edited</param>
        Task PutRequest(string apiUrl, T putObject);

    }
}
