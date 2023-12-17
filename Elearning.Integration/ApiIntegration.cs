namespace Elearning.Integration
{
    using Elearning.Contracts.Helpers;

    public class ApiIntegration<T> where T : class
    {
        public async Task<object> Get(string url)
        {
            return await ApiHelper<object>.Get(url);
        }

        public Task<T> PostRequest(string apiUrl, T postObject)
        {
            throw new NotImplementedException();
        }

        public Task<T> PutRequest(string apiUrl, T putObject)
        {
            throw new NotImplementedException();
        }
    }
}