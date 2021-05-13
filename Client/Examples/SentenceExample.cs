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
		public void SentenceExample()
		{
			RestClient client = new RestClient("http://localhost:5000/api/ProfanityChecker/");
			client.RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
			var request = new RestRequest(Method.POST);
			
			var json = JsonConvert.SerializeObject(new InputSentence() { Sentence = "Abuse foobar pizza."});
			request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);

			var response = client.Execute(request);
			if (response.StatusCode != HttpStatusCode.OK)
				throw new Exception(response.Content);

			var result = JsonConvert.DeserializeObject<CheckResultSuccess>(response.Content);
			Console.WriteLine(result.WordList.Aggregate((prev, current) => $"{prev}, {current}"));
		}
	}
}