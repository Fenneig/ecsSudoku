using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems.Debug
{
    public class FieldDebugSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<Configuration> _config = default;
        private readonly EcsCustomInject<SceneData> _sceneData = default;
        public void Init(IEcsSystems systems)
        {
            for (int i = 0; i < _config.Value.GridHeight; i++)
            {
                string result = "";
                for (int j = 0; j < _config.Value.GridWidth; j++)
                {
                    result += $"{_sceneData.Value.SolvedField[_config.Value.GridHeight - i - 1, j]}, ";
                }

                UnityEngine.Debug.Log(result);
            }
        }
    }
}