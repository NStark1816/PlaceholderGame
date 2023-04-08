using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FOGPIUtilities.StateMachine 
{
    [System.Serializable]
    public class SimpleState
    {
        [Header("State Event")]
        public UnityEvent StateEnter;
        public UnityEvent StateUpdate;
        public UnityEvent StateExit;

        [HideInInspector]
        public SimpleStateMachine simpleStateMachine;
        public virtual void OnStart()
        {
            StateEnter.Invoke();
        }

        public virtual void OnUpdate()
        {
            StateUpdate.Invoke();
        }

        public virtual void OnExit()
        {
            StateExit.Invoke();
        }
    }
}