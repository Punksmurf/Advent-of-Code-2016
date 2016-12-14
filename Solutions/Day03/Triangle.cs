namespace AoC2016.Solutions.Day03
{
    public class Triangle
    {
        private readonly int _a;
        private readonly int _b;
        private readonly int _c;

        public Triangle(int sideA, int sideB, int sideC)
        {
            _a = sideA;
            _b = sideB;
            _c = sideC;
        }

        public bool OrIsIt()
        {
            // Any 2 sides must be larger than the remaining side
            return _a + _b > _c && _a + _c > _b && _b + _c > _a;
        }
    }
}