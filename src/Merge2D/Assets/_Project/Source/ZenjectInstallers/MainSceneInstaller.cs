using Merge2D.Source.Services;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Merge2D.Source
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private Transform _containerForItems;
    
        public override void InstallBindings()
        {
            var worldObjectsCollector = Container.Resolve<IWorldObjectsCollector>();
            worldObjectsCollector.ContainerForItems = _containerForItems;
            
            var tilemapService = Container.Resolve<IGridService>();
            tilemapService.Tilemap = _tilemap;
        }
    }
}