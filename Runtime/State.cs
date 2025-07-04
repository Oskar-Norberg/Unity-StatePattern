using UnityEngine;

namespace ringo.StatePattern
{
    public abstract class State : MonoBehaviour
    {
        protected StateMachine StateMachine;

        public virtual void EnterState(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        public virtual void ExitState() {}

        public virtual void UpdateState(float deltaTime) {}

        public virtual void FixedUpdateState(float fixedDeltaTime) {}
    }
}