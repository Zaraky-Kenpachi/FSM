using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace Game
{
    public class WorkAtMine : AbstractState
    {
        private readonly Miner miner;

        public WorkAtMine(Miner miner)
        {
            this.miner = miner;
        }

        public override void Enter()
        {
            base.Enter();
            if (miner.MLocation != Location.goldmine)
            {
                miner.SendMessageToWifey(MessageType.BobMessage.GoingToMine);
                miner.MLocation = Location.goldmine;
            }
        }

        public override IState Update()
        {
             miner.MIFatigue += 1;
             miner.MIGoldCarried++;
             miner.UpdateMessageBox(MessageType.BobMessage.PickUpNugget);
             if (!miner.Fatigued())
            {
                miner.UpdateMessageBox(MessageType.BobMessage.ReadyToWork);
                return this;
            }
            if(miner.PocketsFull())
            {
                return new VisitBankState(miner);
            }
             if(miner.Thirsty())
                return new DrinkAtBar(miner);
             
             return this;
        }


        public override void Leave()
        {
            base.Leave();
            miner.UpdateMessageBox(MessageType.BobMessage.LeavingMine);
        }
    }
}

