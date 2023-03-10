using UnityEngine;

namespace EcsSudoku.Services
{
    [CreateAssetMenu]
    public class Configuration : ScriptableObject
    {
        public int GridWidth;
        public int GridHeight;
    }
}
