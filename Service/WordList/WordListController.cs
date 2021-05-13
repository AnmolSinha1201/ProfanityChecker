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
		/// <summary>Adds a word to the wordlist</summary>
		/// <response code="200">Word added successfully</response>
		/// <response code="400">Returns error message</response> 
		[HttpPost]
		public IActionResult Post(InputWord Input)
		{
			var words = ProfanityChecker.AddWord(Input.Word);
			return words.Match<IActionResult>(
				success => Ok(success),
				fail => BadRequest(fail)
			);
		}

		/// <summary>Deletes a word from the wordlist</summary>
		/// <param name="Input">Word which is proposed to be deleted</param>
		/// <response code="200">Word deleted successfully. If the word does not exist in wordlist, 200 is returned.</response>
		/// <response code="400">Returns error message</response> 
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
