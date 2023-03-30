using System.Collections.Generic;
using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems
{
    public class UguiButtonsSwitchSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CellAddNumberEvent>> _eventFilter = Idents.Worlds.Events;
        private readonly EcsFilterInject<Inc<Number>> _numberFilter = default;

        private readonly EcsCustomInject<Configuration> _config = default;
        private readonly EcsCustomInject<SceneData> _sceneData = default;

        private Dictionary<int, int> _fieldNumbersCount;

        public void Init(IEcsSystems systems)
        {
            _fieldNumbersCount = new Dictionary<int, int>(_config.Value.GridSize);
            for (int i = 0; i < _config.Value.GridSize; i++)
            {
                _fieldNumbersCount[i + 1] = 0;
            }

            DisableExtraButtons();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var _ in _eventFilter.Value)
            {
                DisableExtraButtons();
            }
        }

        private void DisableExtraButtons()
        {
            for (int i = 0; i < _config.Value.GridSize; i++)
            {
                _fieldNumbersCount[i + 1] = 0;
            }

            foreach (var entity in _numberFilter.Value)
            {
                if (_numberFilter.Pools.Inc1.Get(entity).Value == 0) continue;
                _fieldNumbersCount[_numberFilter.Pools.Inc1.Get(entity).Value]++;
            }

            foreach (var i in _fieldNumbersCount)
            {
                _sceneData.Value.NumberButtons[i.Key - 1].SetActive(i.Value < 9);
            }
        }
    }
}