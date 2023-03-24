using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems
{
    public class GameOverSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Number, Position>> _filter = default;
        
        private readonly EcsCustomInject<SceneData> _sceneData = default;
        private readonly EcsCustomInject<Configuration> _config = default;
        
        public void Init(IEcsSystems systems)
        {
            _sceneData.Value.LoseView.gameObject.SetActive(false);
            _sceneData.Value.WinView.gameObject.SetActive(false);
        }

        public void Run(IEcsSystems systems)
        {
            if (_sceneData.Value.MistakeWasMade == _config.Value.MaxMistakes)
            {
                _sceneData.Value.LoseView.SetActive(true);
                _sceneData.Value.GameOnPause = true;
            }

            bool isWin = true;
            foreach (var entity in _filter.Value)
            {
                var number = _filter.Pools.Inc1.Get(entity).Value;
                var position = _filter.Pools.Inc2.Get(entity).Value;
                if (_sceneData.Value.SolvedField[position.Y, position.X] != number) isWin = false;
            }

            if (isWin)
            {
                _sceneData.Value.WinView.gameObject.SetActive(true);
                string text = "Затраченное время:";
                string timeLast = _sceneData.Value.TimerView.TimerText.text.Split("\r\n")[1];
                _sceneData.Value.WinView.TimeLast.text = $"{text}\r\n{timeLast}";
                _sceneData.Value.GameOnPause = true;
            }
        }
    }
}