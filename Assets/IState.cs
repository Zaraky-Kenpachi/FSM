using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface IState
    {
        void Enter();
        IState Update();
        void Leave();
    }
}
