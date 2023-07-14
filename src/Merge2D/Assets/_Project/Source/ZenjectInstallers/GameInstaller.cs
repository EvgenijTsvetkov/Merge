using Merge2D.Source.Data;
using Merge2D.Source.Game;
using Merge2D.Source.Services;
using Zenject;

namespace Merge2D.Source
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGame();
            BindMainCamera();
            BindObjectsCollector();
            BindItemsCollector();

            BindAssetProvider();
            BindConfigsProvider();

            BindServices();
            BindFactories();
            
            BindItemPool();
        }

        private void BindGame()
        {
            Container.Bind<IGameProvider>().To<GameProvider>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();

            Container.Bind<IInitializable>().To<GameRunner>().AsTransient();
        }

        private void BindMainCamera()
        {
            Container.Bind<IMainCameraProvider>().To<MainCameraProvider>().AsSingle();
            Container.Bind<ICameraFactory>().To<CameraFactory>().AsSingle();
        }

        private void BindAssetProvider()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        }

        private void BindConfigsProvider()
        {
            Container.Bind<IConfigsProvider>().To<ConfigsProvider>().AsSingle();
        }

        private void BindObjectsCollector()
        {
            Container.Bind<IWorldObjectsCollector>().To<WorldObjectsCollector>().AsSingle();
        }

        private void BindItemsCollector()
        {
            Container.Bind<IItemsCollector>().To<ItemsCollector>().AsSingle();
        }

        private void BindItemPool()
        {
            Container.Bind<IItemPool>().To<ItemPool>().AsSingle();
        }

        private void BindServices()
        {
            BindPhysicsCaster();
            BindInputService();
            BindInteractionService();
            BindMergeService();
            BindDragAndDropService();
            BindTilemapService();
        }

        private void BindFactories()
        {
            Container.Bind<IItemFactory>().To<ItemFactory>().AsSingle();
        }

        private void BindPhysicsCaster()
        {
            Container.Bind<IPhysicsCaster>().To<PhysicsCaster>().AsSingle();
        }

        private void BindInputService()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            Container.BindInterfacesTo<StandaloneInputService>().AsSingle();
#else
            Container.BindInterfacesTo<MobileInputService>().AsSingle();
#endif
        }

        private void BindInteractionService()
        {
            Container.BindInterfacesTo<InteractionService>().AsSingle();
        }

        private void BindDragAndDropService()
        {
            Container.BindInterfacesTo<DragAndDropService>().AsSingle();
        }

        private void BindTilemapService()
        {
            Container.Bind<IGridService>().To<GridService>().AsSingle();
        }

        private void BindMergeService()
        {
            Container.Bind<IMergeService>().To<MergeService>().AsSingle();
        }
    }
}