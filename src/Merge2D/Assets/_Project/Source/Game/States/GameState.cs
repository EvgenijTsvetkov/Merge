using Merge2D.Source.StateMachine;
using UnityEngine;

namespace Merge2D.Source.Game
{
    public abstract class GameState : IState
    {
        private readonly IGameProvider _gameProvider;

        protected IGame Game => _gameProvider.Value;
        
        protected GameState(IGameProvider gameProvider)
        {
            _gameProvider = gameProvider;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
    }
}