using System.Threading.Tasks;

namespace AoC2016.Solutions.Day3
{
    public class Solution : ISolution
    {
        public Task<string> SolveExampleAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> SolveSilverAsync(string input)
        {
            var valid = new NormalWall(input).CountValidTriangles();
            return Task.FromResult($"It seems that there are only {valid} valid triangles described on the wall");
        }

        public Task<string> SolveGoldAsync(string input)
        {
            var valid = new IdioticWall(input).CountValidTriangles();
            return Task.FromResult($"Apparently there are {valid} valid triangles described on the wall in a very odd way");
        }
    }
}