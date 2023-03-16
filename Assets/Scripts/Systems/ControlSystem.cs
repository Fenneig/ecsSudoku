using EcsSudoku.Components;
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
        private readonly EcsFilterInject<Inc<Clicked>> _clickedFilter;
        private readonly EcsCustomInject<SceneData> _sceneData;
        private readonly EcsPoolInject<Clicked> _clickedPool;
        private readonly EcsPoolInject<Position> _positionPool;

        private EventsBus _events;

        public void Init(IEcsSystems systems)
        {
            SudokuInput input = new SudokuInput();
            input.Enable();
            input.Sudoku.SetCallbacks(this);
            _events = _sceneData.Value.EventsBus;
        }
        
        public void OnClick(InputAction.CallbackContext context)
        {
            if (!context.canceled) return;
            
            var camera = _sceneData.Value.Camera;
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            
            if (!Physics.Raycast(ray, out var hitInfo)) return;
            
            var cellView = hitInfo.collider.GetComponent<CellView>();
            
            if (!cellView) return;

            foreach (var entity in _clickedFilter.Value)
                _clickedPool.Value.Del(entity);

            _events.NewEventSingleton<CellClickedEvent>().CellPosition = _positionPool.Value.Get(cellView.Entity).Value;
        }
    }
}