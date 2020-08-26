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
        public enum TestType
        {
            TestYouTubeLike,
            TestYouTubeComment,
            TestYouTubeSearch
        }
        /// <summary>
        /// Input 0 for the first case (response should be false, because we cannot set "like" without authorization)
        /// Input 1 for the second case (response should be true)
        /// Input 2 for the third case (response should be true)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> TestCase(CaseModel model)
        {
            var result = await _youTubeLikeService.DoTests(model.Id);
            return Ok(result);
        }
    }
}
