using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class DrinkAtBar : AbstractState
    {
        private readonly Miner miner;

        public DrinkAtBar(Miner miner)
        {
            this.miner = miner;
        }

        public override void Enter()
        {
            base.Enter();
            if (miner.MLocation != Location.saloon)
            {
                miner.UpdateMessageBox(MessageType.BobMessage.GoingToSaloon);
                miner.MLocation = Location.saloon;
            }
        }

        public override IState Update()
        {
            IState nextState = this;
            miner.BuyAndDrinkAWhiskey();
            miner.UpdateMessageBox(MessageType.BobMessage.MightyFineLiquer);
            if (!miner.Fatigued())
            {
                miner.SendMessageToDrunkenFistMaster(MessageType.BobMessage.LetsTrainMaster);
                TrainingWithMaster();
            }
            else
            {
                miner.SendMessageToDrunkenFistMaster(MessageType.BobMessage.LetsDrinkMaster);
                miner.BuyAndDrinkAWhiskey();
                nextState =  new GoHomeAndSleepState(miner);
            }
            return nextState;
        }

        public override void Leave()
        {
            base.Leave();
            miner.SendMessageToWifey(MessageType.BobMessage.LeavingSaloon);
        }
        
        public void TrainingWithMaster()
        {
            miner.MIThirst += 1;
            miner.MIFatigue += 1;
            miner.UpdateMessageBox(MessageType.BobMessage.Asskicked);
        }
    }
}

