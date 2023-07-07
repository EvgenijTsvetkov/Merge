using Merge2D.Source.StateMachine;
using Zenject;

namespace Merge2D.Source.Game
{
    public class GameFactory : IGameFactory
    {
        private readonly IInstantiator _instantiator;

        public GameFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public IGame Create()
        {
            return _instantiator.Instantiate<GameStateMachine>(new object[] {CreateStates()});
        }

        private IState[] CreateStates()
        {
            return new IState[]
            {
                _instantiator.Instantiate<BootState>(),
                _instantiator.Instantiate<GameLoopState>(),
            };
        }
    }
}
