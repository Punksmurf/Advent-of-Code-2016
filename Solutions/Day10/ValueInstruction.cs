namespace AoC2016.Solutions.Day10
{
    public class ValueInstruction : Instruction
    {
        public int Value { get; }

        public ValueInstruction(int botId, int value) : base(botId)
        {
            Value = value;
        }

        public override void Visit(BunnyFactory factory)
        {
            var bot = factory.GetBot(BotId);
            if (!bot.CanAddValue()) return;

            bot.AddValue(Value);
            MarkDone();
        }
    }
}