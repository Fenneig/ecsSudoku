using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsSudoku.Systems
{
    public class InitFieldNumbers : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<Cell, Position>> _filter = default;
        private readonly EcsCustomInject<Configuration> _config = default;

        private readonly EcsPoolInject<Number> _numberPool = default;
        private readonly EcsPoolInject<CellViewRef> _cellViewPool = default;

        public void Init(IEcsSystems systems)
        {
            int[,] field = new int[_config.Value.GridHeight, _config.Value.GridWidth];
            foreach (var entity in _filter.Value)
            {
                var entityPos = _filter.Pools.Inc2.Get(entity).Value;
                field[entityPos.Y, entityPos.X] = entity;
            }

            RandomizeField(30, field);

            for (int y = 0; y < _config.Value.GridHeight; y++)
            {
                for (int x = 0; x < _config.Value.GridWidth; x++)
                {
                    var value = (y * _config.Value.AreaSize + y / _config.Value.AreaSize + x) %
                                   (_config.Value.AreaSize * _config.Value.AreaSize) + 1;

                    _numberPool.Value.Add(field[y, x]).Value = value;
                    
                    _cellViewPool.Value.Get(field[y, x]).Value.Number.text = value.ToString();
                }
            }
        }

        private void RandomizeField(int operationsAmount, int[,] table)
        {
            for (int i = 0; i < operationsAmount; i++)
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
                        TransposeField(table);
                        break;
                }
            }
        }
        
        private void TransposeField(int[,] table)
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
            int area = Random.Range(0, _config.Value.AreaSize);
            int line1 = Random.Range(0, _config.Value.AreaSize);

            int n1 = area * _config.Value.AreaSize + line1;

            int line2;
            do line2 = Random.Range(0, _config.Value.AreaSize);
            while (line1 == line2);

            int n2 = area * _config.Value.AreaSize + line2;

            for (int i = 0; i < _config.Value.GridWidth; i++)
                (table[n1, i], table[n2, i]) = (table[n2, i], table[n1, i]);
        }

        private void SwapColumnsSmall(int[,] table)
        {
            TransposeField(table);
            SwapRowsSmall(table);
            TransposeField(table);
        }

        private void SwapRowsArea(int[,] table)
        {
            int area1 = Random.Range(0, _config.Value.AreaSize);

            int area2;
            do area2 = Random.Range(0, _config.Value.AreaSize);
            while (area1 == area2);

            for (int i = 0; i < _config.Value.AreaSize; i++)
            {
                int n1 = area1 * _config.Value.AreaSize + i;
                int n2 = area2 * _config.Value.AreaSize + i;

                for (int j = 0; j < _config.Value.GridWidth; j++)
                {
                    (table[n1, j], table[n2, j]) = (table[n2, j], table[n1, j]);
                }
            }
        }

        private void SwapColumnsArea(int[,] table)
        {
            TransposeField(table);
            SwapRowsArea(table);
            TransposeField(table);
        }
    }
}