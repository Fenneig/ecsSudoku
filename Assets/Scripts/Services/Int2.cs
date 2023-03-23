namespace EcsSudoku.Services
{
    public readonly struct Int2
    {
        public readonly int X;
        public readonly int Y;

        public Int2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"Position x = {X}, y= {Y}";
        }
    }
}