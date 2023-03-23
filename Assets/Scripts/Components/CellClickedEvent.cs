using EcsSudoku.Services;

namespace EcsSudoku.Components
{
    public struct CellClickedEvent
    {
        public Int2 Position;
        public int Number;
    }
}