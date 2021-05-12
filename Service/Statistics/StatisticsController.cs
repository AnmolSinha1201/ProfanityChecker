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
	public class StatisticsController : ControllerBase
	{
		[HttpGet]
		public IActionResult Get()
		{
			var result = ProfanityChecker.GetOverallStatistic();
			return result.Match<IActionResult>(
				success => Ok(success.ToString()),
				fail => BadRequest(fail.ToString())
			);
		}
	}
}
