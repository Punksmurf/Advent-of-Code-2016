namespace AoC2016.Solutions.Day08.Instructions
{
    public class RotateColumn : Rotate
    {
        public int Column { get; }

        public RotateColumn(int column, int amount) : base(amount)
        {
            Column = column;
        }

        public override void Visit(Display display)
        {
            display.RotateColumn(Column, Amount);
        }
    }
}