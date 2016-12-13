namespace AoC2016.Solutions.Day10
{
    public class GiveInstruction : Instruction
    {
        public ReceiverType LowerReceiverType { get; }
        public int LowerReceiverId { get; }

        public ReceiverType HigherReceiverType { get; }
        public int HigherReceiverId { get; }

        public GiveInstruction(int giverId, ReceiverType lowerReceiverType, int lowerReceiverId,
            ReceiverType higherReceiverType, int higherReceiverId) : base(giverId)
        {
            LowerReceiverType = lowerReceiverType;
            LowerReceiverId = lowerReceiverId;

            HigherReceiverType = higherReceiverType;
            HigherReceiverId = higherReceiverId;
        }

        public override void Visit(BunnyFactory factory)
        {
            var giver = factory.GetBot(BotId);
            if (!giver.CanTakeValues()) return;

            var lowerReceiver = factory.GetValueReceiver(LowerReceiverType, LowerReceiverId);
            var higherReceiver = factory.GetValueReceiver(HigherReceiverType, HigherReceiverId);

            if (!lowerReceiver.CanAddValue() || !higherReceiver.CanAddValue()) return;

            var values = giver.TakeValues();
            lowerReceiver.AddValue(values[0]);
            higherReceiver.AddValue(values[1]);

            MarkDone();
        }
    }
}