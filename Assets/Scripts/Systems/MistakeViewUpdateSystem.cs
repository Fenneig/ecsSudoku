using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems
{
    public class MistakeViewUpdateSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<SceneData> _sceneData = default;
        private readonly EcsCustomInject<Configuration> _config = default;
        
        public void Run(IEcsSystems systems)
        {
            _sceneData.Value.MistakeView.TmpText.text =
                $"Ошибки\r\n{_sceneData.Value.MistakeWasMade}/{_config.Value.MaxMistakes}";
        }
    }
}