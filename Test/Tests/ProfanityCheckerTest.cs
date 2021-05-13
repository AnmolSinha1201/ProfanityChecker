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
	public class ProfanityCheckerTest
	{
		[Fact]
		public void SentenceTest()
		{
			RestClient client = new RestClient("http://localhost:5000/api/ProfanityChecker/");
			client.RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
			var request = new RestRequest(Method.POST);
			
			var json = JsonConvert.SerializeObject(new InputSentence() { Sentence = "Abuse foobar pizza."});
			request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);

			var response = client.Execute(request);
			Assert.Equal(response.StatusCode, HttpStatusCode.OK);

			var result = JsonConvert.DeserializeObject<CheckResultSuccess>(response.Content);
			Assert.Equal(result.WordList.Count, 1);
			Assert.Equal(result.WordList[0], "abuse");
		}

		[Fact]
		public void FileTest()
		{
			var filePath = @"Foobar.txt";
			RestClient client = new RestClient("http://localhost:5000/api/ProfanityChecker/Upload");
			client.RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
			var request = new RestRequest(Method.POST);

			FileInfo fileInfo = new FileInfo(filePath);
			long fileLength = fileInfo.Length;

			request.AddHeader("Content-Length", fileLength.ToString());
			request.AddHeader("accept", "*/*");
			request.AddHeader("Content-Type", "multipart/form-data");
			request.AlwaysMultipartFormData = true;
			request.AddFile("file", filePath, "text/plain");

			var response = client.Execute(request);
			Assert.Equal(response.StatusCode, HttpStatusCode.OK);

			var result = JsonConvert.DeserializeObject<CheckResultSuccess>(response.Content);
			Assert.Equal(result.WordList.Count, 1);
			Assert.Equal(result.WordList[0], "abuse");
		}

	}
}