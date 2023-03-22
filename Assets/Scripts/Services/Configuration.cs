using EcsSudoku.Views;
using UnityEngine;

namespace EcsSudoku.Services
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        [Header("Field settings")]
        public int GridWidth;
        public int GridHeight;
        public int AreaSize;
        public Vector2 Offset;
        [Space] [Header("Prefabs")]
        public CellView CellViewPrefab;
        public GameObject CellAreaPrefab;
        public GameObject NumberButtonPrefab;
    }
}
