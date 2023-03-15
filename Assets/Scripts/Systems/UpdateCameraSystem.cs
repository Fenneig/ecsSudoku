using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsSudoku.Systems
{
    public class UpdateCameraSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<SceneData> _sceneData;
        private readonly EcsCustomInject<Configuration> _config;

        public void Init(IEcsSystems systems)
        {
            var height = _config.Value.GridHeight;
            var width = _config.Value.GridWidth;
            var camera = _sceneData.Value.Camera;
            camera.orthographic = true;
            var orthographicSizeHeight = (height + (height - 1) * _config.Value.Offset.y) / 2;
            var orthographicSizeWidth = (width + (width - 1) * _config.Value.Offset.x) / 2;

            camera.orthographicSize = _config.Value.GridWidth * (1 + _config.Value.Offset.x) *
                                      Screen.height / Screen.width / 2f;

            _sceneData.Value.CameraTransform.position =
                new Vector3(orthographicSizeWidth, orthographicSizeHeight);
        }
    }
}