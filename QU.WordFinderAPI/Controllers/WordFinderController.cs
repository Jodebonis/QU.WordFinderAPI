using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QU.WordFinderAPI.Domain.Models;
using QU.WordFinderAPI.Interfaces;

namespace QU.WordFinderAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WordFinderController : ControllerBase
    {
        private readonly ILogger<WordFinderController> _logger;

        public WordFinderController(ILogger<WordFinderController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Provides functionality for finding words within a matrix.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpPost(Name = "WordFinder")]
        public async Task<IResult> PostWordFinder(WordFinder wordFinder, [FromServices] IWordFinderService wordFinderService)
        {
            IEnumerable<string> result = await wordFinderService.FindAsync(wordFinder);
            return Results.Ok(result);
        }


    }
}
