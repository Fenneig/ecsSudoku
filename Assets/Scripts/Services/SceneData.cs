using UnityEngine;

namespace EcsSudoku.Services
{
    public class SceneData : MonoBehaviour
    {
        public Transform CameraTransform;
        public Camera Camera;
        public Transform FieldTransform;
        public Transform NumberButtonsTransform;
        public int[,] SolvedField;
        public int Difficult = 30;
    }
}
