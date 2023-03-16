using EcsSudoku.Services;

namespace EcsSudoku.Components
{
    public struct CellClickedEvent : IEventSingleton
    {
        public Int2 CellPosition;
    }
}