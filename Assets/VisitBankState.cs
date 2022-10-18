using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class VisitBankState :  AbstractState
    {
        private readonly Miner miner;
        public VisitBankState(Miner miner)
        {
            this.miner = miner;
        }
    
        public override void Enter()
        {
            base.Enter();
            if (miner.MLocation != Location.bank)
            {
                miner.UpdateMessageBox(MessageType.BobMessage.GoingToBank);
                miner.MLocation = Location.bank;
            }
        }
    
        public override IState Update()
        {
            IState nextState;
            if (miner.PocketsFull())
            {
                miner.UpdateMessageBox(MessageType.BobMessage.DepositAtBank);
                miner.AddToWealth(miner.MIGoldCarried);
                miner.MIGoldCarried = 0;
            }

            if (miner.Wealth() >= miner.ComfortLevel)
            {
                miner.UpdateMessageBox(MessageType.BobMessage.Rich);
                nextState = new GoHomeAndSleepState(miner);
            }
            else if (miner.Thirsty())
                nextState = new DrinkAtBar(miner);
            else if (miner.Fatigued())
                nextState =  new GoHomeAndSleepState(miner);
            else
                nextState = new WorkAtMine(miner);

            return nextState;
        }
        
        public override void Leave()
        {
            base.Leave();
            miner.UpdateMessageBox(MessageType.BobMessage.LeaveBank);
        }
    }
}

