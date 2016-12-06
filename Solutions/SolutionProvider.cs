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
                default:
                    throw new System.NotImplementedException();
            }
        }
    }
}