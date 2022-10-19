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
                miner.UpdateMessageBox(MessageType.BobMessage.GoingToMine);
                miner.MLocation = Location.goldmine;
            }
        }

        public override IState Update()
        {
            IState nextState = this;
            miner.MIFatigue += 1;
            miner.MIGoldCarried++;
            miner.UpdateMessageBox(MessageType.BobMessage.PickUpNugget);
             
            if(miner.PocketsFull())
            {
                nextState = new VisitBankState(miner);
            }
            else if(miner.Thirsty())
                nextState = new DrinkAtBar(miner);
            else miner.UpdateMessageBox(MessageType.BobMessage.LetgetMoreNugget);
            
            return nextState;
        }
        
        public override void Leave()
        {
            base.Leave();
            miner.UpdateMessageBox(MessageType.BobMessage.LeavingMine);
        }
    }
}

