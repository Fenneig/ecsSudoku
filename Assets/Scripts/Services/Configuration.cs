using EcsSudoku.Views;
using UnityEngine;

namespace EcsSudoku.Services
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        public int GridWidth;
        public int GridHeight;
        public int AreaSize;
        public CellView CellViewPrefab;
        public GameObject CellAreaPrefab;
        public GameObject NumberButtonPrefab;
        public Vector2 Offset;
    }
}
