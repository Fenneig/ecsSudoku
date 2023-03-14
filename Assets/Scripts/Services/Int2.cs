namespace EcsSudoku.Services
{
    public struct Int2
    {
        public int X;
        public int Y;

        public override string ToString()
        {
            return $"Cell coords is {X},{Y}";
        }
    }
}