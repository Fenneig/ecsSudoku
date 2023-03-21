using EcsSudoku.Components;
using EcsSudoku.Views;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine.Scripting;

namespace EcsSudoku.Systems
{
    public class TestUguiClickEventSystem : EcsUguiCallbackSystem
    {
        private readonly EcsFilterInject<Inc<Clicked, Number>, Exc<SolvedCell>> _filter = default;
        private readonly EcsPoolInject<PlacedNumber> _placedPool = default;

        [Preserve]
        [EcsUguiClickEvent("NumberButton")]
        private void OnButtonClick(in EcsUguiClickEvent e)
        {
            var number = int.Parse(e.Sender.GetComponent<NumberButtonView>().NumberText.text);
           
            foreach (var entity in _filter.Value)
            {
                _filter.Pools.Inc2.Get(entity).Value = number;
                
                if (!_placedPool.Value.Has(entity))
                    _placedPool.Value.Add(entity);
            }
        }
    }
}