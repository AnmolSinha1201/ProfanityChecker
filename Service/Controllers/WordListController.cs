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
	public class WordListController : ControllerBase
	{
		[HttpPost]
		public IActionResult Post(WordInput Input)
		{
			var words = ProfanityChecker.AddWord(Input.Word);
			return words.Match<IActionResult>(
				success => Ok(success.ToString()),
				fail => BadRequest(fail.ToString())
			);
		}

		[HttpDelete("{Input}")]
		public IActionResult Delete(string Input)
		{
			var result = ProfanityChecker.RemoveWord(Input);
			return result.Match<IActionResult>(
				success => Ok(success.ToString()),
				fail => BadRequest(fail.ToString())
			);
		}
	}
}
