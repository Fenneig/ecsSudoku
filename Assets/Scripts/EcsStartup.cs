using EcsSudoku.Services;
using EcsSudoku.Systems;
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
        [SerializeField] private EcsUguiEmitter _uguiEmitter;
        
        EcsWorld _world;
        IEcsSystems _systems;

        void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems
                .Add(new GameplayFieldInitSystem())
                .Add(new CellControlSystem())
                .Add(new AnalyzeSelectedCellSystem())
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Inject(_sceneData, _config)
                .InjectUgui(_uguiEmitter)
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