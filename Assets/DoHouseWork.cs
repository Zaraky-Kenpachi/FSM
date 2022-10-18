using System.Collections;
using System.Collections.Generic;
using Game;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class DoHouseWork : AbstractState
    {
        private readonly Wifey Elsa;
        public DoHouseWork(Wifey elsa)
        {
            this.Elsa = elsa;
        }
    
        public override void Enter()
        {
        }
    
        public override IState Update()
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    Elsa.UpdateMessageBox(MessageType.ElsaMessage.Moppin);
                    return this;
                case 1:
                    Elsa.UpdateMessageBox(MessageType.ElsaMessage.Washin);
                    return this;
                case 2:
                    Elsa.UpdateMessageBox(MessageType.ElsaMessage.MakinBed);
                    return this;
                default:
                    return this;
            }
        }
            
        public override void Leave()
        {
        }
    }
}
