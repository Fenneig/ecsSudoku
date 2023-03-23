namespace EcsSudoku.Services
{
    public struct Int2
    {
        public int X;
        public int Y;

        public Int2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"Position x = {X}, y= {Y}";
        }
        
        public static bool operator ==(Int2 obj1, Int2 obj2) =>
            obj1.X == obj2.X && obj1.Y == obj2.Y;

        public static bool operator !=(Int2 obj1, Int2 obj2) => 
            !(obj1 == obj2);
    }
}