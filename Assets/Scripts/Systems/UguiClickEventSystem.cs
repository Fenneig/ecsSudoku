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

        private readonly EcsPoolInject<CellAddNumberEvent> _cellAddNumberEventPool = Idents.Worlds.Events;
        private readonly EcsPoolInject<CellEraseEvent> _cellErasedEventPool = Idents.Worlds.Events;

        private readonly EcsCustomInject<SceneData> _sceneData = default;

        [Preserve]
        [EcsUguiClickEvent("NumberButton")]
        private void OnButtonClick(in EcsUguiClickEvent e)
        {
            foreach (var entity in _filter.Value)
            {
                var number = int.Parse(e.Sender.GetComponent<NumberButtonView>().NumberText.text);

                ref var cellAddNumberEvent =
                    ref _cellAddNumberEventPool.Value.Add(_cellAddNumberEventPool.Value.GetWorld().NewEntity());
                
                cellAddNumberEvent.Number = number;
                cellAddNumberEvent.CellEntity = entity;
            }
        }

        [Preserve]
        [EcsUguiClickEvent("RestartButton")]
        private void OnRestartClick(in EcsUguiClickEvent e)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        [Preserve]
        [EcsUguiClickEvent("NotesButton")]
        private void OnNotesClicked(in EcsUguiClickEvent e)
        {
            _sceneData.Value.NoteMode = !_sceneData.Value.NoteMode;
            _sceneData.Value.NotesStateText.text = _sceneData.Value.NoteMode ? "ON" : "OFF";
            _sceneData.Value.NotesStateText.color = _sceneData.Value.NoteMode
                ? Idents.Colors.ButtonTextSwitchOn
                : Idents.Colors.ButtonTextSwitchOff;
        }

        [Preserve]
        [EcsUguiClickEvent("EraseButton")]
        private void OnEraseClicked(in EcsUguiClickEvent e)
        {
            foreach (var entity in _filter.Value)
            {
                _cellErasedEventPool.Value.Add(_cellErasedEventPool.Value.GetWorld().NewEntity()).CellEntity = entity;
            }
        }
    }
}