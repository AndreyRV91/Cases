using System.Threading.Tasks;
using Cases.Domain.Contracts;
using Cases.Domain.Implementations.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cases.Controllers
{
    [ApiController]
    [Route("controller")]
    public class CasesController : ControllerBase
    {
        private readonly IYouTubeTestService _youTubeLikeService;

        public CasesController(IYouTubeTestService youTubeLikeService)
        {
            _youTubeLikeService = youTubeLikeService;
        }

        [HttpPost]
        public async Task<IActionResult> TestCase(CaseModel model)
        {
            var result = _youTubeLikeService.DoTests(model.Id);
            return Ok(result);
        }
    }
}
