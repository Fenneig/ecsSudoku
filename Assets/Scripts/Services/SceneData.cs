using UnityEngine;

namespace EcsSudoku.Services
{
    public class SceneData : MonoBehaviour
    {
        [SerializeField] private RectTransform _gameplayPanelTransform;
        public RectTransform GameplayPanelTransform => _gameplayPanelTransform;
    }
}
