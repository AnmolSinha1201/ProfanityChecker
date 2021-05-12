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
	public class ProfanityCheckerController : ControllerBase
	{
		[HttpGet]
		public int Get()
		{
			return 1;
		}

		[HttpPost]
		public IActionResult Post(ServiceInput Input)
		{
			var words = ProfanityChecker.Check(Input.Sentence);
			return Ok(words);
		}
	}
}
