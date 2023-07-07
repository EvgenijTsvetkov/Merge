using System;
using System.Collections.Generic;

namespace Merge2D.Source.StateMachine
{
    public class BaseStateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IState> _states = new Dictionary<Type, IState>(8);
       
        public IState CurrentState { get; private set; }
        
        public void SwitchState<TState>() where TState : class, IState
        {
            CurrentState?.Exit();
            CurrentState = GetState<TState>();
            CurrentState.Enter();
        }

        protected void AddState(IState state)
        {
            if (state == null)
                throw new ArgumentNullException($"Argument state doesn't exist");

            _states.Add(state.GetType(), state);
        }

        private TState GetState<TState>() where TState : class, IState
        {
            if(_states.ContainsKey(typeof(TState)) == false)
                throw new InvalidOperationException($"Cannot find {typeof(TState)}. " +
                                                    "Add it when creating a state machine");
            
            return _states[typeof(TState)] as TState;
        }
    }
}
