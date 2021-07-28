using System.Net.Http;
using System.Threading.Tasks;

namespace BankingTest.Wrappers
{
    public class HttpClientWrapper : IHttpClientWrapper
	{
		private readonly HttpClient HttpClient;
		public HttpClientWrapper()
		{
			HttpClient = new HttpClient();
		}

		public Task<HttpResponseMessage> GetAsync(string url)
		{
			return HttpClient.GetAsync(url);
		}

		public Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
		{
			return HttpClient.PostAsync(url, content);
		}

		public Task<HttpResponseMessage> PutAsync(string url, HttpContent content)
		{
			return HttpClient.PutAsync(url, content);
		}

		public Task<HttpResponseMessage> DeleteAsync(string url)
		{
			return HttpClient.DeleteAsync(url);
		}

		public void Dispose()
		{
			HttpClient?.Dispose();
		}
	}
}
