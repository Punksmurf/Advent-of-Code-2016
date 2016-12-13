namespace AoC2016.Solutions.Day8.Instructions
{
    public class RotateRow : Rotate
    {
        public int Row { get; }

        public RotateRow(int row, int amount) : base(amount)
        {
            Row = row;
        }

        public override void Visit(Display display)
        {
            display.RotateRow(Row, Amount);
        }
    }
}