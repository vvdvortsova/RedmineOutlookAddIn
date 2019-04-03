using System;
using System.Net;

namespace Redmine.Net.Api
{
	/// <summary>
	/// </summary>
	public class RedmineWebClient : WebClient
	{
		private readonly CookieContainer _container = new CookieContainer();

		protected override WebRequest GetWebRequest(Uri address)
		{
			Headers.Add(HttpRequestHeader.Cookie, "redmineCookie");
			var wr = base.GetWebRequest(address);
			var httpWebRequest = wr as HttpWebRequest;

			if (httpWebRequest == null) return wr;

			httpWebRequest.CookieContainer = _container;
			httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip |
													DecompressionMethods.Deflate;
			return httpWebRequest;
		}
	}
}
