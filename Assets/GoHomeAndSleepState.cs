using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace Game
{
    public class GoHomeAndSleepState : AbstractState
    {
        private readonly Miner miner;

        public GoHomeAndSleepState(Miner miner)
        {
            this.miner = miner;
        }

        public override void Enter()
        {
            base.Enter();
            if (miner.MLocation != Location.shack)
            {
                miner.SendMessageToWifey(MessageType.BobMessage.HiHoneyImHome);
                miner.MLocation = Location.shack;
            }
        }

        public override IState Update()
        {
            IState nextState = this;
            if (!miner.Fatigued())
            {
                miner.UpdateMessageBox(MessageType.BobMessage.ReadyToWork);
                nextState =  new WorkAtMine(miner);
            }
            else if (miner.Fatigued())
            {
                miner.DecreaseFatigue();
                miner.UpdateMessageBox(MessageType.BobMessage.Sleeping);
                nextState = this;
            }

            if (miner.Wealth() > miner.ComfortLevel)
                nextState = new DrinkAtBar(miner);
            
            return nextState;
        }

    public override void Leave()
        {
            base.Leave();
            miner.SendMessageToWifey(MessageType.BobMessage.LeavingShack);
        }
    }
}
