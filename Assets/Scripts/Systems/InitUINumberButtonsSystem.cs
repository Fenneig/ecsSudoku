using EcsSudoku.Services;
using EcsSudoku.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsSudoku.Systems
{
    public class InitUINumberButtonsSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<Configuration> _config = default;
        private readonly EcsCustomInject<SceneData> _sceneData = default;
        
        public void Init(IEcsSystems systems)
        {
            _sceneData.Value.NumberButtons = new GameObject[_config.Value.GridSize];
            for (int i = 0; i <  _config.Value.GridSize; i++)
            {
                var numberButton = Object.Instantiate(_config.Value.NumberButtonPrefab, _sceneData.Value.NumberButtonsTransform);
                numberButton.GetComponent<NumberButtonView>().NumberText.text = (i + 1).ToString();
                _sceneData.Value.NumberButtons[i] = numberButton;
            }
        }
    }
}