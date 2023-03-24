using EcsSudoku.Components;
using EcsSudoku.Services;
using EcsSudoku.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EcsSudoku.Systems
{
    public class ControlSystem : IEcsInitSystem, IEcsDestroySystem, SudokuInput.ISudokuActions
    {
        private readonly EcsFilterInject<Inc<Clicked>> _clickedFilter = default;
        
        private readonly EcsCustomInject<SceneData> _sceneData = default;

        private SudokuInput _input;

        public void Init(IEcsSystems systems)
        {
            _input = new SudokuInput();
            _input.Enable();
            _input.Sudoku.SetCallbacks(this);
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

            _clickedFilter.Pools.Inc1.Add(cellView.Entity);
        }

        public void Destroy(IEcsSystems systems)
        {
            _input.Sudoku.Disable();
        }
    }
}