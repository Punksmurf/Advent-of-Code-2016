namespace AoC2016.Solutions.Day10
{
    public interface IValueReceiver
    {
        bool CanAddValue();
        void AddValue(int value);
    }
}