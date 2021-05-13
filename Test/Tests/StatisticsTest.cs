using Xunit;
using Service;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Globalization;
using System.IO;
using RestSharp;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System;
using System.Net;

namespace Test
{
	public class StatisticsTest
	{
		[Fact]
		public void ShouldReturnOK()
		{
			RestClient client = new RestClient("https://localhost:5001/api/Statistics");
			client.RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
			var request = new RestRequest(Method.GET);
			var response = client.Execute(request);

			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}
	}
}