using EcsSudoku.Components;
using EcsSudoku.Services;
using EcsSudoku.Views;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

namespace EcsSudoku.Systems
{
    public class UguiClickEventSystem : EcsUguiCallbackSystem
    {
        private readonly EcsFilterInject<Inc<Clicked, Number, Position>, Exc<SolvedCell>> _filter = default;

        private readonly EcsPoolInject<PlacedNumber> _placedPool = default;
        private readonly EcsPoolInject<CellClickedEvent> _cellClickedEventPool = Idents.Worlds.Events;

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

                ref var cellClickedEvent =
                    ref _cellClickedEventPool.Value.Add(_cellClickedEventPool.Value.GetWorld().NewEntity());
                cellClickedEvent.Number = number;
                cellClickedEvent.Position = _filter.Pools.Inc3.Get(entity).Value;
            }
        }

        [Preserve]
        [EcsUguiClickEvent("RestartButton")]
        private void OnRestartClick(in EcsUguiClickEvent e)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}