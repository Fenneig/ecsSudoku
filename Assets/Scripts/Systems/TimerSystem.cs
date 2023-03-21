using EcsSudoku.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsSudoku.Systems
{
    public class TimerSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<SceneData> _sceneData = default;

        private float _timePassed;
        
        public void Run(IEcsSystems systems)
        {
            _timePassed += Time.deltaTime;
            int minutes = (int) _timePassed / 60;
            int seconds = (int) _timePassed % 60;
            string formattedMinutes = minutes >= 10 ? $"{minutes}" : $"0{minutes}";
            string formattedSeconds = seconds >= 10 ? $"{seconds}" : $"0{seconds}";
            string formattedTime = $"Время\r\n{formattedMinutes}:{formattedSeconds}";

            _sceneData.Value.TimerView.TimerText.text = formattedTime;
        }
    }
}