﻿using EcsSudoku.Components;
using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsSudoku.Systems
{
    public class PlaceNumberSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CellClickedEvent>> _cellClickedEventFilter = Idents.Worlds.Events;

        private readonly EcsFilterInject<Inc<Number>, Exc<SolvedCell>> _filter = default;

        private readonly EcsCustomInject<SceneData> _sceneData = default;

        public void Run(IEcsSystems systems)
        {
            if (_sceneData.Value.NoteMode) return;
            
            foreach (var eventEntity in _cellClickedEventFilter.Value)
            {
                var eventInfo = _cellClickedEventFilter.Pools.Inc1.Get(eventEntity);
                
                _filter.Pools.Inc1.Get(eventInfo.CellEntity).Value = eventInfo.Number;
            }
        }
    }
}