using System.Collections.Generic;
using UnityEngine;

namespace EcsSudoku.Services
{
    public class SceneData : MonoBehaviour
    {
        public RectTransform GameplayPanelTransform;

        public Dictionary<Int2, int> Cells = new Dictionary<Int2, int>();
        public int[,] SolvedTable { get; private set; }
        
        public void InitSolvedTable(int[,] solvedState)
        {
            SolvedTable = solvedState;
        }

    }
}
