using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Service.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProfanityCheckerController : ControllerBase
	{
		[HttpPost]
		public IActionResult Post(InputSentence Input)
		{
			var result = ProfanityChecker.Check(Input.Sentence);
			return result.Match<IActionResult>(
				success => Ok (success.ToString()),
				fail => BadRequest(fail.ToString())
			);
		}

		[HttpPost]
		[Route("Upload")]
		public IActionResult Upload(IFormFile file)
		{
			var fileName = System.IO.Path.GetFileName(file.FileName);
			
			using(var uploadedFile = file.OpenReadStream())
			{
				StreamReader reader = new StreamReader( uploadedFile );
				var content = reader.ReadToEnd();
				return Post(new InputSentence() {Sentence = content });
			}
		}
	}
}
