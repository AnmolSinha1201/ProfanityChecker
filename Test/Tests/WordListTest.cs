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
	public class WordListTest
	{
		[Fact]
		public void MultipleWordTest()
		{
			var word = "Abuse foobar pizza.";
			RestClient client = new RestClient("http://localhost:5000/api/WordList/");
			client.RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
			var addRequest = new RestRequest(Method.POST);
			
			var json = JsonConvert.SerializeObject(new InputWord() { Word = word });
			addRequest.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
			var addResponse = client.Execute(addRequest);
			Assert.Equal(HttpStatusCode.BadRequest, addResponse.StatusCode);

			var deleteRequest = new RestRequest(word, Method.DELETE);
			var deleteResponse = client.Execute(deleteRequest);
			Assert.Equal(HttpStatusCode.BadRequest, deleteResponse.StatusCode);
		}

		[Fact]
		public void AddAndRemoveTest()
		{
			var word = "Foobar";
			RestClient client = new RestClient("http://localhost:5000/api/WordList/");
			client.RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
			var addRequest = new RestRequest(Method.POST);
			
			var json = JsonConvert.SerializeObject(new InputWord() { Word = word});
			addRequest.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
			var addResponse = client.Execute(addRequest);
			Assert.Equal(HttpStatusCode.OK, addResponse.StatusCode);

			var deleteRequest = new RestRequest(word, Method.DELETE);
			var deleteResponse = client.Execute(deleteRequest);
			Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
		}

	}
}