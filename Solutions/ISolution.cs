using System.Threading.Tasks;

namespace AoC2016.Solutions
{
    public interface ISolution
    {
        int GetDay();
        Task<string> SolveSilverAsync(string input);
        Task<string> SolveGoldAsync(string input);
    }
}