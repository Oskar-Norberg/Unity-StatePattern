using System;
using System.Collections.Generic;
using UnityEngine;

namespace ringo.StatePattern
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
        
        protected Dictionary<Type, State> _stateDictionary = new();
        protected State currentState;

        protected virtual void Awake()
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
        
            Debug.LogWarning("State not found");
        }

        public void AddState<TState>(TState state) where TState : State
        {
            if (_stateDictionary.ContainsKey(state.GetType()))
            {
                Debug.Log("State already exists");
                return;
            }
        
            _stateDictionary.Add(state.GetType(), state);
        }

        protected virtual void UpdateState()
        {
            currentState?.UpdateState(Time.deltaTime);
        }

        protected virtual void FixedUpdateState()
        {
            currentState?.FixedUpdateState(Time.fixedDeltaTime);
        }
    
        protected virtual void SwitchState(State state)
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