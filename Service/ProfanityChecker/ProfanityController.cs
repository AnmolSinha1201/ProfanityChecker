using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Service.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProfanityCheckerController : ControllerBase
	{
		/// <summary>Scans a sentence and produces a list of profane words among other details</summary>
		/// <response code="200">Returns a list of profane words</response>
		/// <response code="400">Returns error message</response> 
		[HttpPost]
		public IActionResult Post(InputSentence Input)
		{
			var result = ProfanityChecker.Check(Input.Sentence);
			return result.Match<IActionResult>(
				success => Ok (success),
				fail => BadRequest(fail)
			);
		}

		/// <summary>Scans a file and produces a list of profane words among other details</summary>
		/// <response code="200">Returns a list of profane words</response>
		/// <response code="400">Returns error message</response> 
		[HttpPost]
		[Route("Upload")]
		public IActionResult Upload(IFormFile File)
		{
			if (File == null)
				return BadRequest(new Failure() { Description = "Failed to upload file" });

			var fileName = System.IO.Path.GetFileName(File.FileName);
			
			using(var uploadedFile = File.OpenReadStream())
			{
				StreamReader reader = new StreamReader(uploadedFile);
				var content = reader.ReadToEnd();
				return Post(new InputSentence() {Sentence = content });
			}
		}
	}
}
