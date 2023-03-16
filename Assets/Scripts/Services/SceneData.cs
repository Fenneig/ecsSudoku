using UnityEngine;

namespace EcsSudoku.Services
{
    public class SceneData : MonoBehaviour
    {
        public Transform CameraTransform;
        public Camera Camera;
        public Transform FieldTransform;
        public EventsBus EventsBus;
        public int[,] SolvedField;
    }
}
