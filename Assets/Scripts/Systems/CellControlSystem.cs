using EcsSudoku.Components;
using EcsSudoku.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;

namespace EcsSudoku.Systems
{
    public class CellControlSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsPoolInject<Clicked> _clickedPool;
        private EcsPoolInject<EcsUguiClickEvent> _clickEventPool;
        private EcsFilter _clickEvents;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _clickEvents = world.Filter<EcsUguiClickEvent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _clickEvents)
            {
                ref EcsUguiClickEvent data = ref _clickEventPool.Value.Get(entity);
                var cellEntity = data.Sender.GetComponent<CellView>().Entity;

                _clickedPool.Value.Add(cellEntity);
            }
        }
    }
}