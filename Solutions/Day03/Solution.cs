using System.Threading.Tasks;

namespace AoC2016.Solutions.Day03
{
    public class Solution : ISolution
    {
        public Task<string> SolveExampleAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> SolveSilverAsync(string input)
        {
            var valid = await Task.Run(() => new NormalWall(input).CountValidTriangles());
            return $"It seems that there are only {valid} valid triangles described on the wall";
        }

        public async Task<string> SolveGoldAsync(string input)
        {
            var valid = await Task.Run(() => new IdioticWall(input).CountValidTriangles());
            return $"Apparently there are {valid} valid triangles described on the wall in a very odd way";
        }
    }
}