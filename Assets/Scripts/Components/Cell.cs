using EcsSudoku.Services;
using EcsSudoku.Views;

namespace EcsSudoku.Components
{
    public struct Cell
    {
        public CellView View;
        public Int2 Coords;
        public int Value;
    }
}