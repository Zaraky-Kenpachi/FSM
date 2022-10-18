using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class AbstractState : IState
    {   public virtual void Enter()
        {
            //Nothing to do by default.
        }

        public abstract IState Update();

        public virtual void Leave()
        {
            //Nothing to do by default.
        }
    }
}