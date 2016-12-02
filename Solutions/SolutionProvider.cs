﻿namespace AoC2016.Solutions
{
    public class SolutionProvider
    {
        public static ISolution GetSolution(int day)
        {
            switch (day)
            {
                case 1: return new Day1.Solution();
                case 2: return new Day2.Solution();
                default:
                    throw new System.NotImplementedException();
            }
        }
    }
}