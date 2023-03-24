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

        [Space] [Header("Scene views")]
        public TimerView TimerView;
        public MistakeView MistakeView;
        
        [Space] [Header("Scene data fills during game")] 
        public int[,] SolvedField;
        [HideInInspector]
        public int MistakeWasMade = 0;
        [HideInInspector] 
        public GameObject[] NumberButtons;
        [HideInInspector] 
        public bool GameOnPause = false;

        [Space] [Header("Game over views")] 
        public WinView WinView;
        public GameObject LoseView;

        private void Awake()
        {
            CameraTransform = Camera.main.transform;
            Camera = Camera.main;
        }
    }
}
