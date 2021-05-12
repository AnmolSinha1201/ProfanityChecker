using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
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
	}
}
