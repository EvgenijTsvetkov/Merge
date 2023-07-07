using UnityEngine;

namespace Merge2D.Source.Game
{
    public class GameLoopState : GameState
    {
        public GameLoopState(IGameProvider gameProvider) : base(gameProvider)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            Debug.LogWarning("Enter to GameLoopState");
        }
    }
}