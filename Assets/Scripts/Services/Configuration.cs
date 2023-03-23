using EcsSudoku.Views;
using UnityEngine;

namespace EcsSudoku.Services
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        [Header("Field settings")]
        public int GridSize;
        public int AreaSize;
        public float Offset;
        [Space] [Header("Prefabs")]
        public CellView CellViewPrefab;
        public GameObject CellAreaPrefab;
        public GameObject NumberButtonPrefab;
    }
}
