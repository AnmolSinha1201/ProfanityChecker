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
		public void AddAndRemoveExample()
		{
			var word = "Foobar";
			RestClient client = new RestClient("http://localhost:5000/api/WordList/");
			client.RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
			var addRequest = new RestRequest(Method.POST);
			
			// Add a text
			var json = JsonConvert.SerializeObject(new InputWord() { Word = word});
			addRequest.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
			var addResponse = client.Execute(addRequest);
			if (addResponse.StatusCode != HttpStatusCode.OK)
				throw new Exception(addResponse.Content);

			// Remove a text
			var deleteRequest = new RestRequest(word, Method.DELETE);
			var deleteResponse = client.Execute(deleteRequest);
			if (deleteResponse.StatusCode != HttpStatusCode.OK)
				throw new Exception(deleteResponse.Content);
		}
	}
}