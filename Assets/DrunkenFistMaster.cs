using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class DrunkenFistMaster : MonoBehaviour
    {
        public event DFMasterMessage OnDFMasterMessageSentToMiner;
        private int nmbOfSipTaken = 0;

        public int NmbOfSipTaken
        {
            get => nmbOfSipTaken;
            set => nmbOfSipTaken = value;
        }

        [SerializeField] private Miner miner;
        private FiniteStateMachine stateMachine;
        [SerializeField] private GameObject messageBox;
        private Text messageBoxText;
        private string name = "Sensei";
        private void Awake()
        {
            stateMachine = new FiniteStateMachine(new DrinkState(this));
            messageBox = GameObject.Find("DrunkenMessageBox");
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
            switch (bobMessageReceived)
            {
                case MessageType.BobMessage.LetsTrainMaster:
                    UpdateMessageBox(MessageType.DrunkenFistMaster.GetReady);
                    break;
                case MessageType.BobMessage.LetsDrinkMaster:
                    UpdateMessageBox(MessageType.DrunkenFistMaster.DrinkForTheSoul);
                    break;
            }
        }

        public void UpdateMessageBox(MessageType.DrunkenFistMaster DFMasterMessageToSend)
        {
            messageBoxText.text = GetMessageString(DFMasterMessageToSend);
        }

        private string GetMessageString(MessageType.DrunkenFistMaster masterMessage)
        {
            switch (masterMessage)
            {
                case MessageType.DrunkenFistMaster.OneSip:
                    return "One sip for focus.";
                case MessageType.DrunkenFistMaster.TwoSip:
                    return "Two sip for courage";
                case MessageType.DrunkenFistMaster.ThreeSip:
                    return "Three sip for strength";
                case MessageType.DrunkenFistMaster.DragonBreath:
                    return "Unleash the dragon flame!";
                case MessageType.DrunkenFistMaster.GetReady:
                    return "Sure lil pupil, get ready for some ass'whooping";
                case MessageType.DrunkenFistMaster.DrinkForTheSoul:
                    return "Let meditate on this whisky";
                default:
                    return "Invalid string in DrunkenFistMasterMessage";
            }
        }

        public void DrinkOneSip()
        {
            UpdateMessageBox(MessageType.DrunkenFistMaster.OneSip);
            nmbOfSipTaken++;
        }
        public void DrinkTwoSip()
        {
            UpdateMessageBox(MessageType.DrunkenFistMaster.TwoSip);
            nmbOfSipTaken++;

        }
        public void DrinkThreeSip()
        {
            UpdateMessageBox(MessageType.DrunkenFistMaster.ThreeSip);
            nmbOfSipTaken++;

        }
        public void DragonBreath()
        {
            UpdateMessageBox(MessageType.DrunkenFistMaster.DragonBreath);
            nmbOfSipTaken = 0;
        }
        
        
        
        
        public IEnumerator UpdateInBackground()
        {
            yield return new WaitForSeconds(3);
            stateMachine.Update();
            StartCoroutine(UpdateInBackground());
        }
    }
}
public delegate void DFMasterMessage(MessageType.DrunkenFistMaster DFMasterMessageSent);