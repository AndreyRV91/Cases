using System.Threading.Tasks;

namespace Cases.Domain.Contracts
{
    public interface IYouTubeLikeService
    {
        Task<bool> DoTests(int id);
    }
}