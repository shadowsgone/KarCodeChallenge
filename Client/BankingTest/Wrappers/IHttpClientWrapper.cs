using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BankingTest.Wrappers
{
	public interface IHttpClientWrapper : IDisposable
	{
		Task<HttpResponseMessage> GetAsync(string url);
		Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
		Task<HttpResponseMessage> PutAsync(string url, HttpContent content);
		Task<HttpResponseMessage> DeleteAsync(string url);
	}
}
