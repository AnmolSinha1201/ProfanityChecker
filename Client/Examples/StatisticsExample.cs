using System;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using RestSharp;
using Service;

namespace Client
{
	public partial class Examples
	{
		public void StatisticsExample()
		{
			RestClient client = new RestClient("https://localhost:5001/api/Statistics");
			client.RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
			var request = new RestRequest(Method.GET);
			var response = client.Execute(request);

			if (response.StatusCode != HttpStatusCode.OK)
				throw new Exception(response.Content);

			var result = JsonConvert.DeserializeObject<OverallStatisticSuccess>(response.Content);
			var popularWords = result.PopularWords.Select(i => i.Word).Aggregate((prev, current) => $"{prev}, {current}");
			Console.WriteLine($"Popular words : {popularWords}");
		}
	}
}