using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class DrinkState : AbstractState
    {
        private readonly DrunkenFistMaster master;
       
        public DrinkState(DrunkenFistMaster master)
        {
            this.master = master;
        }
    
        public override void Enter()
        {
        }
    
        public override IState Update()
        {
            IState nextState = this;
            if (master.NmbOfSipTaken == 0)
                master.DrinkOneSip();
            else if (master.NmbOfSipTaken == 1)
                master.DrinkTwoSip();
            else if (master.NmbOfSipTaken == 2)
                master.DrinkThreeSip();
            else
                master.DragonBreath();
            
            return nextState;
        }
    
        public override void Leave()
        {
        }
    }
}

