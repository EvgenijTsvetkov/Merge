using Merge2D.Source.Game;
using Zenject;

namespace Merge2D.Source
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGame();
        }
        
        private void BindGame()
        {
            Container.Bind<IGameProvider>().To<GameProvider>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            
            Container.Bind<IInitializable>().To<GameRunner>().AsSingle();
        }
    }
}
