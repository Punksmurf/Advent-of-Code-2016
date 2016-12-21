namespace AoC2016.Solutions
{
    public class SolutionProvider
    {
        public static ISolution GetSolution(int day)
        {
            switch (day)
            {
                case 1: return new Day01.Solution();
                case 2: return new Day02.Solution();
                case 3: return new Day03.Solution();
                case 4: return new Day04.Solution();
                case 5: return new Day05.Solution();
                case 6: return new Day06.Solution();
                case 7: return new Day07.Solution();
                case 8: return new Day08.Solution();
                case 9: return new Day09.Solution();
                case 10: return new Day10.Solution();
                case 11: return new Day11.Solution();
                case 12: return new Day12.Solution();
                case 13: return new Day13.Solution();
                case 14: return new Day14.Solution();
                case 15: return new Day15.Solution();
                case 16: return new Day16.Solution();
                case 17: return new Day17.Solution();
                case 18: return new Day18.Solution();
                case 19: return new Day19.Solution();
                case 20: return new Day20.Solution();
                case 21: return new Day21.Solution();
                default:
                    throw new System.NotImplementedException();
            }
        }
    }
}