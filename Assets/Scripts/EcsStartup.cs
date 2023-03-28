using EcsSudoku.Components;
using EcsSudoku.Services;
using EcsSudoku.Systems;
using EcsSudoku.Systems.Debug;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.Unity.Ugui;
using UnityEngine;

namespace EcsSudoku
{
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private SceneData _sceneData;
        [SerializeField] private Configuration _config;
        [SerializeField] private EcsUguiEmitter _emitter;

        EcsWorld _world;
        IEcsSystems _systems;

        void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems
                .AddWorld(new EcsWorld(), Idents.Worlds.Events)
                
                .Add(new InitFieldSystem())
                .Add(new InitCellViewSystem())
                .Add(new InitAreaSystem())
                .Add(new InitFieldNumbersSystem())
                .Add(new InitEraseExtraFieldNumbersSystem())
                .Add(new InitCameraSystem())
                .Add(new InitUINumberButtonsSystem())
                
                .Add(new ControlSystem())
                .Add(new AnalyzeClickSystem())
                .Add(new MarkSameNumbersSystem())
                .Add(new RecolorCellsSystem())

                .Add(new UguiClickEventSystem())
                .Add(new PlaceNoteSystem())
                .Add(new PlaceNumberSystem())
                .Add(new UguiButtonsSwitchSystem())
                .Add(new FillFieldWithNumbersSystem())
                .Add(new MistakeViewUpdateSystem())
                .DelHere<CellClickedEvent>(Idents.Worlds.Events)
                
                .Add(new MarkMistakeCellsSystem())
                
                .Add(new TimerSystem())
                .Add(new GameOverSystem())
                
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new FieldDebugSystem())
#endif
                .Inject(_sceneData, _config)
                .InjectUgui(_emitter)
                .Init();
        }

        void Update()
        {
            _systems?.Run();
        }

        void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
            }
            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}