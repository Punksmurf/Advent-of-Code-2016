using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2016.Solutions.Day10
{
    public class BunnyFactory
    {
        private readonly Dictionary<int, Bot> _bots = new Dictionary<int, Bot>();
        private readonly Dictionary<int, Output> _outputs = new Dictionary<int, Output>();

        public IEnumerable<Bot> FindBotsWhichCompared(int a, int b)
        {
            var low = Math.Min(a, b);
            var high = Math.Max(a, b);
            return _bots.Values.Where(_ => _.DidCompare(low, high));
        }

        public Bot GetBot(int botId)
        {
            if (!_bots.ContainsKey(botId))
            {
                _bots.Add(botId, new Bot(botId));
            }
            return _bots[botId];
        }

        public Output GetOutput(int outputId)
        {
            if (!_outputs.ContainsKey(outputId))
            {
                _outputs.Add(outputId, new Output());
            }
            return _outputs[outputId];
        }

        public IValueReceiver GetValueReceiver(ReceiverType type, int id)
        {
            switch (type)
            {
                case ReceiverType.Bot:
                    return GetBot(id);
                case ReceiverType.Output:
                    return GetOutput(id);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void Accept(Instruction instruction)
        {
            instruction.Visit(this);
        }
    }
}