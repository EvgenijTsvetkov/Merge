using System.Threading.Tasks;

namespace Merge2D.Source.Game
{
    public class BootState : GameState
    {
        public BootState(IGameProvider gameProvider) : base(gameProvider)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            
            GameToGameLoopState();
        }

        private void GameToGameLoopState()
        {
            Game.SwitchState<GameLoopState>();
        }
    }
}
