using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace EcsSudoku.Systems
{
    public class GameplayFieldInitSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<SceneData> _sceneData = default;
        private readonly EcsCustomInject<Configuration> _config = default;

        private readonly EcsPoolInject<Cell> _cellPool = default;

        private int _dimensionSize = 3;

        public void Init(IEcsSystems systems)
        {
            var world = _cellPool.Value.GetWorld();
            var cellViewPrefab = _config.Value.CellView;
            var cellSize = cellViewPrefab.GetComponent<RectTransform>().rect.width;
            var table = new int[_config.Value.GridWidth, _config.Value.GridHeight];

            for (int i = 0; i < _config.Value.GridWidth; i++)
            {
                for (int j = 0; j < _config.Value.GridHeight; j++)
                {
                    table[i, j] = (i * _dimensionSize + i / _dimensionSize + j) % (_dimensionSize * _dimensionSize) + 1;
                }
            }

            RandomizeTable(30, table);

            for (int i = 0; i < _config.Value.GridWidth; i++)
            {
                for (int j = 0; j < _config.Value.GridHeight; j++)
                {
                    var posX = j * cellSize;
                    var posY = (_config.Value.GridHeight - i - 1) * cellSize;
                    var createPosition = new Vector3(posX, posY);
                    var cellView = Object.Instantiate(cellViewPrefab,
                                                      createPosition,
                                                      Quaternion.identity,
                                                      _sceneData.Value.GameplayPanelTransform);
                    cellView.NumberText.text = table[i, j].ToString();

                    var entity = world.NewEntity();
                    ref var cell = ref _cellPool.Value.Add(entity);
                    cell.View = cellView;
                    cell.Coords = new Int2 {X = j, Y = j};
                    cell.Value = table[i, j];

                    _sceneData.Value.Cells[cell.Coords] = entity;
                    cellView.Entity = entity;
                }
            }

            for (int i = 0; i < _dimensionSize; i++)
            {
                for (int j = 0; j < _dimensionSize; j++)
                {
                    var posX = i * cellSize * _dimensionSize;
                    var posY = j * cellSize * _dimensionSize;
                    var createPosition = new Vector3(posX, posY);
                    Object.Instantiate(_config.Value.AreaPrefab,
                                       createPosition,
                                       quaternion.identity,
                                       _sceneData.Value.GameplayPanelTransform);
                }
            }

            _sceneData.Value.InitSolvedTable(table);

            //TODO erase extra numbers functional
        }

        private void RandomizeTable(int operationsCount, int[,] table)
        {
            for (int i = 0; i < operationsCount; i++)
            {
                var function = Random.Range(0, 5);
                switch (function)
                {
                    case 1:
                        SwapRowsSmall(table);
                        break;
                    case 2:
                        SwapColumnsSmall(table);
                        break;
                    case 3:
                        SwapRowsArea(table);
                        break;
                    case 4:
                        SwapColumnsArea(table);
                        break;
                    default:
                        TransposeTable(table);
                        break;
                }
            }
        }

        private void TransposeTable(int[,] table)
        {
            for (int i = 0; i < _config.Value.GridWidth; i++)
            {
                for (int j = i; j < _config.Value.GridHeight; j++)
                {
                    (table[i, j], table[j, i]) = (table[j, i], table[i, j]);
                }
            }
        }

        private void SwapRowsSmall(int[,] table)
        {
            int area = Random.Range(0, _dimensionSize);
            int line1 = Random.Range(0, _dimensionSize);

            int n1 = area * _dimensionSize + line1;

            int line2;
            do line2 = Random.Range(0, _dimensionSize);
            while (line1 == line2);

            int n2 = area * _dimensionSize + line2;

            for (int i = 0; i < _config.Value.GridWidth; i++)
                (table[n1, i], table[n2, i]) = (table[n2, i], table[n1, i]);
        }

        private void SwapColumnsSmall(int[,] table)
        {
            TransposeTable(table);
            SwapRowsSmall(table);
            TransposeTable(table);
        }

        private void SwapRowsArea(int[,] table)
        {
            int area1 = Random.Range(0, _dimensionSize);

            int area2;
            do area2 = Random.Range(0, _dimensionSize);
            while (area1 == area2);

            for (int i = 0; i < _dimensionSize; i++)
            {
                int n1 = area1 * _dimensionSize + i;
                int n2 = area2 * _dimensionSize + i;

                for (int j = 0; j < _config.Value.GridWidth; j++)
                {
                    (table[n1, j], table[n2, j]) = (table[n2, j], table[n1, j]);
                }
            }
        }

        private void SwapColumnsArea(int[,] table)
        {
            TransposeTable(table);
            SwapRowsArea(table);
            TransposeTable(table);
        }
    }
}