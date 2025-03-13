using System;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern
{
    /**
     * <summary>
     * Simple State Machine.
     * To tick behaviour tree call the UpdateState and FixedUpdateState methods in the derived class.
     * </summary>
     */
    public abstract class StateMachine : MonoBehaviour
    {
        [SerializeField] private State startState;
        [SerializeField] private State[] states;
        
        private Dictionary<Type, State> _stateDictionary = new();
    
        protected State currentState;

        protected void Awake()
        {
            InitializeStateDictionary(states);
            
            SwitchState(startState);
        }

        public void SwitchState<TNextState>()
        {
            if (_stateDictionary.TryGetValue(typeof(TNextState), out State state))
            {
                SwitchState(state);
                return;
            }
        
            Debug.Log("State not found");
        }
        {
        }

        protected virtual void UpdateState()
        {
            currentState?.UpdateState(Time.deltaTime);
        }

        protected virtual void FixedUpdateState()
        {
            currentState?.FixedUpdateState(Time.fixedDeltaTime);
        }
    
        private void SwitchState(State state)
        {
            currentState?.ExitState();
            currentState = state;
            state.EnterState(this);
        }
        
        private void InitializeStateDictionary(State[] states)
        {
            foreach (State state in states)
            {
                _stateDictionary.Add(state.GetType(), state);
            }
        }
    }
}