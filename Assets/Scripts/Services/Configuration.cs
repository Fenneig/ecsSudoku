using EcsSudoku.Views;
using UnityEngine;

namespace EcsSudoku.Services
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        public int GridWidth;
        public int GridHeight;
        public CellView CellViewPrefab;
        public GameObject CellAreaPrefab;
        public Vector2 Offset;
    }
}
