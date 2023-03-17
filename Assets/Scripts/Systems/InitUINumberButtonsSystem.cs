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
            var maxLineSize = _config.Value.GridWidth > _config.Value.GridHeight
                ? _config.Value.GridWidth
                : _config.Value.GridHeight;
            
            for (int i = 0; i < maxLineSize; i++)
            {
                var numberButton = GameObject.Instantiate(_config.Value.NumberButtonPrefab, _sceneData.Value.NumberButtonsTransform);
                numberButton.GetComponent<NumberButtonView>().NumberText.text = (i + 1).ToString();
            }
        }
    }
}