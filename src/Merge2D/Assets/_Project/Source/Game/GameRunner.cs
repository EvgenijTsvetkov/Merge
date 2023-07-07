namespace Merge2D.Source.Game
{
    public class GameRunner : IGameRunner
    {
        private readonly IGameFactory _gameFactory;
        private readonly IGameProvider _gameProvider;

        public GameRunner(IGameFactory gameFactory, IGameProvider gameProvider)
        {
            _gameFactory = gameFactory;
            _gameProvider = gameProvider;
        }

        public void Initialize()
        {
            _gameProvider.Value = _gameFactory.Create();
            _gameProvider.Value.SwitchState<BootState>();
        }
    }
}