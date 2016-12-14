namespace AoC2016.Solutions.Day08.Instructions
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