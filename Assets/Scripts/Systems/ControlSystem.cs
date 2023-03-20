﻿using EcsSudoku.Components;
using EcsSudoku.Services;
using EcsSudoku.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EcsSudoku.Systems
{
    public class ControlSystem : IEcsInitSystem, SudokuInput.ISudokuActions
    {
        private readonly EcsFilterInject<Inc<Clicked>> _clickedFilter = default;
        private readonly EcsFilterInject<Inc<CellClickedEvent>> _cellClickedEventFilter = default;

        
        private readonly EcsCustomInject<SceneData> _sceneData = default;
        
        private readonly EcsPoolInject<Position> _positionPool = default;

        public void Init(IEcsSystems systems)
        {
            SudokuInput input = new SudokuInput();
            input.Enable();
            input.Sudoku.SetCallbacks(this);
        }
        
        public void OnClick(InputAction.CallbackContext context)
        {
            if (!context.canceled) return;
            
            var ray = _sceneData.Value.Camera.ScreenPointToRay(Input.mousePosition);
            
            if (!Physics.Raycast(ray, out var hitInfo)) return;
            
            var cellView = hitInfo.collider.GetComponent<CellView>();
            
            if (!cellView) return;

            foreach (var entity in _clickedFilter.Value)
                _clickedFilter.Pools.Inc1.Del(entity);
            
            foreach (var entity in _cellClickedEventFilter.Value)
                _cellClickedEventFilter.Pools.Inc1.Del(entity);
            
            _cellClickedEventFilter.Pools.Inc1.Add(cellView.Entity).CellPosition =
                _positionPool.Value.Get(cellView.Entity).Value;
        }
    }
}