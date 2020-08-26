using System.Threading.Tasks;

namespace Cases.Domain.Contracts
{
    public interface IYouTubeTestService
    {
        Task<bool> DoTests(int id);
    }
}