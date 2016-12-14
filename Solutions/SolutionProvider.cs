namespace AoC2016.Solutions
{
    public class SolutionProvider
    {
        public static ISolution GetSolution(int day)
        {
            switch (day)
            {
                case 1: return new Day1.Solution();
                case 2: return new Day2.Solution();
                case 3: return new Day3.Solution();
                case 4: return new Day4.Solution();
                case 5: return new Day5.Solution();
                case 6: return new Day6.Solution();
                case 7: return new Day7.Solution();
                case 8: return new Day8.Solution();
                case 9: return new Day9.Solution();
                case 10: return new Day10.Solution();
                case 11: return new Day11.Solution();
                case 12: return new Day12.Solution();
                case 13: return new Day13.Solution();
                case 14: return new Day14.Solution();
                default:
                    throw new System.NotImplementedException();
            }
        }
    }
}