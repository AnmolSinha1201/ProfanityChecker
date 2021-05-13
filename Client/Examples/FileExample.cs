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
		public void FileExample()
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
			if (response.StatusCode != HttpStatusCode.OK)
				throw new Exception(response.Content);

			var result = JsonConvert.DeserializeObject<CheckResultSuccess>(response.Content);
			Console.WriteLine(result.WordList.Aggregate((prev, current) => $"{prev}, {current}"));
		}
	}
}