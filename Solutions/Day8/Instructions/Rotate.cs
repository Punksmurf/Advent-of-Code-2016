namespace AoC2016.Solutions.Day8.Instructions
{
    public abstract class Rotate : Instruction
    {
        public int Amount { get; }

        protected Rotate(int amount)
        {
            Amount = amount;
        }
    }
}