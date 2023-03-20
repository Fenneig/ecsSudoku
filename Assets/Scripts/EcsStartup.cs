using EcsSudoku.Services;
using EcsSudoku.Systems;
using EcsSudoku.Systems.Debug;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
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
            _sceneData.EventsBus = new EventsBus();
            _systems = new EcsSystems(_world);
            _systems
                // register your systems here, for example:
                // .Add (new TestSystem1 ())
                // .Add (new TestSystem2 ())
                .Add(new InitFieldSystem())
                .Add(new InitCellViewSystem())
                .Add(new InitAreaSystem())
                .Add(new InitFieldNumbersSystem())
                .Add(new InitEraseExtraFieldNumbersSystem())
                .Add(new UpdateCameraSystem())
                .Add(new ControlSystem())
                .Add(new ClickAnalyzeSystem())
                .Add(new MarkSameNumbersSystem())
                .Add(new RecolorCellsSystem())
                .Add(new FillFieldWithNumbersSystem())
                .Add(new InitUINumberButtonsSystem())
                .Add(new TestUguiClickEventSystem())

                // register additional worlds here, for example:
                // .AddWorld (new EcsWorld (), "events")
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new FieldDebugSystem())
#endif
                .Inject(_sceneData, _config)
                .InjectUgui(_emitter)
                .Init();
        }

        void Update()
        {
            // process systems here.
            _systems?.Run();
        }

        void OnDestroy()
        {
            if (_systems != null)
            {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy();
                _systems = null;
            }

            // cleanup custom worlds here.

            // cleanup default world.
            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}