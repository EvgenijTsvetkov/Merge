using Merge2D.Source.StateMachine;

namespace Merge2D.Source.Game
{
    public class GameStateMachine : BaseStateMachine, IGame
    {
        public GameStateMachine(IState[] states)
        {
            foreach (var state in states) 
                AddState(state);
        }
    }
}
