using System.Threading.Tasks;

namespace AoC2016.Solutions
{
    public interface ISolution
    {
        Task<string> SolveExampleAsync();
        Task<string> SolveSilverAsync(string input);
        Task<string> SolveGoldAsync(string input);
    }
}