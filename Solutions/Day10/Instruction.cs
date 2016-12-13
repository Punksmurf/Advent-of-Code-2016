using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC2016.Solutions.Day10
{
    public abstract class Instruction
    {
        public static class Factory
        {
            private const string ValuePattern = @"value (?<value>\d+) goes to bot (?<bot>\d+)";
            private const string GiverPattern = @"bot (?<giver>\d+) gives low to (?<low_type>bot|output) (?<low_id>\d+) and high to (?<high_type>bot|output) (?<high_id>\d+)";

            public static List<Instruction> CreateInstructions(string instructions)
            {
                return instructions.Split('\n').Select(Create).ToList();
            }

            public static Instruction Create(string instruction)
            {
                var match = Regex.Match(instruction, ValuePattern);
                if (match.Success) return CreateValueInstruction(match);

                match = Regex.Match(instruction, GiverPattern);
                if (match.Success) return CreateGiveInstruction(match);

                throw new Exception($"Unable to parse instruction {instruction}");
            }

            private static ValueInstruction CreateValueInstruction(Match valueMatch)
            {
                var bot = int.Parse(valueMatch.Groups["bot"].Value);
                var value = int.Parse(valueMatch.Groups["value"].Value);
                return new ValueInstruction(bot, value);
            }

            private static GiveInstruction CreateGiveInstruction(Match giveMatch)
            {
                var giver = int.Parse(giveMatch.Groups["giver"].Value);

                var lowType = giveMatch.Groups["low_type"].Value == "bot" ? ReceiverType.Bot : ReceiverType.Output;
                var lowId = int.Parse(giveMatch.Groups["low_id"].Value);

                var highType = giveMatch.Groups["high_type"].Value == "bot" ? ReceiverType.Bot : ReceiverType.Output;
                var highId = int.Parse(giveMatch.Groups["high_id"].Value);

                return new GiveInstruction(giver, lowType, lowId, highType, highId);
            }
        }

        public int BotId { get; }
        public bool Done { get; private set; }

        protected Instruction(int botId)
        {
            BotId = botId;
        }

        public void MarkDone()
        {
            Done = true;
        }

        public abstract void Visit(BunnyFactory factory);
    }
}