using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    public abstract class StateMachine : MonoBehaviour
    {
        [SerializeField] private State startState;
        [SerializeField] private List<State> states = new();
    
        protected State currentState;

        protected void Awake()
        {
            SwitchState(startState);
        }

        public void SwitchState<TNextState>()
        {
            foreach (State state in states)
            {
                if (state.GetType() != typeof(TNextState)) continue;
            
                SwitchState(state);
                return;
            }
        
            Debug.Log("State not found");
        }
    
        private void SwitchState(State state)
        {
            currentState?.ExitState();
            currentState = state;
            state.EnterState(this);
        }

        protected virtual void UpdateState()
        {
            currentState?.UpdateState(Time.deltaTime);
        }

        protected virtual void FixedUpdateState()
        {
            currentState?.FixedUpdateState(Time.fixedDeltaTime);
        }
    }
}