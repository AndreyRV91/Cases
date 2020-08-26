using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Cases.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cases.Controllers
{
    [ApiController]
    [Route("controller")]
    public class CasesController : ControllerBase
    {
        private readonly ILogger<CasesController> _logger;
        private readonly IYouTubeLikeService _youTubeLikeService;

        public CasesController(ILogger<CasesController> logger, IYouTubeLikeService youTubeLikeService)
        {
            _logger = logger;
            _youTubeLikeService = youTubeLikeService;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Case(int id)
        {
            _youTubeLikeService.DoTests(id);

            return Ok();
        }
    }
}
