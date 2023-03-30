using System.Collections.Generic;

namespace EcsSudoku.Components
{
    public struct PlayerMove
    {
        public AffectedCell AffectedCell;
        public List<AffectedNote> AffectedNotes;
    }
}