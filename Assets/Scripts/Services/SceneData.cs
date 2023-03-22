using EcsSudoku.Views;
using UnityEngine;

namespace EcsSudoku.Services
{
    public class SceneData : MonoBehaviour
    {
        [Header("Camera settings")]
        public Transform CameraTransform;
        public Camera Camera;
        [Space] [Header("Transforms for instantiate objects")]
        public Transform FieldTransform;
        public Transform NumberButtonsTransform;
        [Space] [Header("Game settings")]
        public int Difficult = 30;
        [Space] [Header("Scene views")]
        public TimerView TimerView;
        public MistakeView MistakeView;
        [Space] [Header("Scene data fills during game")]
        public int[,] SolvedField;
    }
}
