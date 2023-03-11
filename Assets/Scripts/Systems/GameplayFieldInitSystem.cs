using EcsSudoku.Services;
using EcsSudoku.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EcsSudoku.Systems
{
    public class GameplayFieldInitSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<SceneData> _sceneData = default;
        private readonly EcsCustomInject<Configuration> _config = default;


        public void Init(IEcsSystems systems)
        {
            var cellPrefab = Resources.Load<CellView>("CellView");
            var cellSize = cellPrefab.GetComponent<RectTransform>().rect.width;

            for (int i = 0; i < _config.Value.GridWidth; i++)
            {
                for (int j = 0; j < _config.Value.GridHeight; j++)
                {
                    var cell = Object.Instantiate(cellPrefab,
                        new Vector3(j * cellSize + j % 3 * 5 , i * cellSize + i % 3 * 5),
                        Quaternion.identity,
                        _sceneData.Value.GameplayPanelTransform);
                }
            }
        }
    }
}