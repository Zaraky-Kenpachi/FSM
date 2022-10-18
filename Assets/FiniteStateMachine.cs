using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class FiniteStateMachine
    {
        private IState currentState;

        public FiniteStateMachine(IState startState)
        {
            currentState = startState;
            currentState.Enter();
        }

        public void Update()
        {
            var nextState = currentState.Update();

            if (nextState != currentState)
            {
                currentState?.Leave();
                currentState = nextState;
                currentState?.Enter();
            }
            //StartCoroutine((WaitForSeconds()));
        }
        
        
        public IEnumerator WaitForSeconds()
        {
            yield return new WaitForSeconds(1);
            
        }
    }
}