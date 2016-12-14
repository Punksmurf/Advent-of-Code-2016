namespace AoC2016.Solutions.Day08.Instructions
{
    public class Rect : Instruction
    {
        public int Width { get; }
        public int Height { get; }

        public Rect(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override void Visit(Display display)
        {
            display.Rect(Width, Height);
        }

    }
}