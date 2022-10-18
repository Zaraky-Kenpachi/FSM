using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Game
{
    public class Wifey : MonoBehaviour
    {
        public event WifeyMessage OnWifeyMessageSentToMiner;

        [SerializeField] private Miner miner;
        private FiniteStateMachine stateMachine;
        [SerializeField] private GameObject messageBox;
        private Text messageBoxText;
        private string name = "Elsa";
        Location m_Location;
        private void Awake()
        {
            stateMachine = new FiniteStateMachine(new DoHouseWork(this));
            messageBox = GameObject.Find("WifeMessageBox");
            messageBoxText = messageBox.GetComponent<UnityEngine.UI.Text>();

        }

        // Start is called before the first frame update
        void Start()
        {
            if (miner != null)
                miner.OnMinerMessageSentToWifey += ReactToMessage;
            StartCoroutine(UpdateInBackground());
        }

        public void ReactToMessage(MessageType.BobMessage bobMessageReceived)
        {

        }

        public void UpdateMessageBox(MessageType.ElsaMessage ElsaMessageToSend)
        {
            messageBoxText.text = GetMessageString(ElsaMessageToSend);
        }

        private string GetMessageString(MessageType.ElsaMessage bobMessageToFind)
        {
            switch (bobMessageToFind)
            {
                case MessageType.ElsaMessage.StewReady:
                    return "Bob! StewReady! Lets eat";
                case MessageType.ElsaMessage.PutStewOnTable:
                    return "Puttin' the stew on the table";
                case MessageType.ElsaMessage.FussinOverFood:
                    return "Fussin' over food";
                case MessageType.ElsaMessage.PutStewInOven:
                    return "Putting the stew in the oven";
                case MessageType.ElsaMessage.LeavingBathroom:
                    return "Leavin' the Jon";
                case MessageType.ElsaMessage.Relief:
                    return "Ahhhhhh! Sweet relief!";
                case MessageType.ElsaMessage.Makeup:
                    return "Walkin' to the can. Need to powda mah pretty li'lle nose";
                case MessageType.ElsaMessage.MakinBed:
                    return "Makin' the bed";
                case MessageType.ElsaMessage.Washin:
                    return "Washin' the dishes";
                case MessageType.ElsaMessage.Moppin:
                    return "Moppin' the floor";
                case MessageType.ElsaMessage.TimeToDoHouseWork:
                    return "Time to do some more housework!";
                default:
                    return "Invalid string in ElsaMessage";
            }
        }
        
        public IEnumerator UpdateInBackground()
        {
            yield return new WaitForSeconds(3);
            
            stateMachine.Update();
            StartCoroutine(UpdateInBackground());
        }
    }
}


public delegate void WifeyMessage(MessageType.ElsaMessage elsaMessageSent);