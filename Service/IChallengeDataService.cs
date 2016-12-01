using System.Threading.Tasks;

namespace AoC2016.Service
{
    public interface IChallengeDataService
    {
        Task<string> GetAsync(int day);
    }
}