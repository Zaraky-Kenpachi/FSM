using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{ public class Miner : MonoBehaviour
    {
        public event MinerMessage OnMinerMessageSentToWifey;
        public event MinerMessage OnMinerMessageSentToDrunkenFistMaster;

        private FiniteStateMachine stateMachine;
        [SerializeField]
        private GameObject messageBox;
        private Text messageBoxText;
        private string name = "Bob";
        Location         m_Location;

        private Wifey elsa;
        private DrunkenFistMaster masta;
        
        //the amount of gold a miner must have before he feels he can go home
        public int ComfortLevel = 5;
        //the amount of nuggets a miner can carry
        public int MaxNuggets = 3;
        //above this value a miner is thirsty
        public int ThirstLevel = 5;
        //above this value a miner is sleepy
        public int TirednessThreshold = 5;
        //above this value a miner is hungry
        public int HungernessThreshold = 30;
        
        //how many nuggets the miner has in his pockets
        int                   m_iGoldCarried;
        
        int                   m_iMoneyInBank;

        //the higher the value, the thirstier the miner
        int                   m_iThirst;

        //the higher the value, the more tired the miner
        int                   m_iFatigue;

        private bool isSleeping = false;
        public bool continueUpdate = true;

        public bool ContinueUpdate
        {
            get => continueUpdate;
            set => continueUpdate = value;
        }

        public FiniteStateMachine StateMachine
        {
            get => stateMachine;
            set => stateMachine = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public Location MLocation
        {
            get => m_Location;
            set => m_Location = value;
        }

        public int MIGoldCarried
        {
            get => m_iGoldCarried;
            set => m_iGoldCarried = value;
        }

        public int MIMoneyInBank
        {
            get => m_iMoneyInBank;
            set => m_iMoneyInBank = value;
        }

        public int MIThirst
        {
            get => m_iThirst;
            set => m_iThirst = value;
        }

        public int MIFatigue
        {
            get => m_iFatigue;
            set => m_iFatigue = value;
        }

        public bool IsSleeping
        {
            get => isSleeping;
            set => isSleeping = value;
        }
        private void Awake()
        {
            stateMachine = new FiniteStateMachine(new GoHomeAndSleepState(this));
            messageBox = GameObject.Find("BobMessageBox");
            messageBoxText = messageBox.GetComponent<UnityEngine.UI.Text>();
        }

        public void SendMessageToWifey(MessageType.BobMessage bobMessageToSend)
        {
            if (OnMinerMessageSentToWifey != null)
            {
                UpdateMessageBox(bobMessageToSend);
                OnMinerMessageSentToWifey(bobMessageToSend);
            }
            else
            {
                messageBox.GetComponent<UnityEngine.UI.Text>().text = "BUGGGG";
            }
        }
        
        public void SendMessageToDrunkenFistMaster(MessageType.BobMessage bobMessageToSend)
        {
            if (OnMinerMessageSentToDrunkenFistMaster != null)
            {
                UpdateMessageBox(bobMessageToSend);
                OnMinerMessageSentToDrunkenFistMaster(bobMessageToSend);
            }
        }
        public void UpdateMessageBox(MessageType.BobMessage bobMessageToSend)
        {
            messageBoxText.text = GetMessageString(bobMessageToSend);
        }

        private string GetMessageString(MessageType.BobMessage bobMessageToFind)
        {
            switch (bobMessageToFind)
            {
                case MessageType.BobMessage.HiHoneyImHome:
                    return "Hun, I'm Home";
                case MessageType.BobMessage.ComingHome:
                    return "Okay Hun, ahm a comin'!";
                case MessageType.BobMessage.StewReady:
                    return "Bob, stew is ready, come eat!";
                case MessageType.BobMessage.SmellGood:
                    return "Smells Reaaal goood Elsa!";
                case MessageType.BobMessage.TasteGood:
                    return "Tastes real good too!";
                case MessageType.BobMessage.ThankHun:
                    return "Thankya li'lle lady. Ah better get back to whatever ah wuz doin'";
                case MessageType.BobMessage.GoingToSaloon:
                    return "Boy, ah sure is thusty! Walking to the saloon";
                case MessageType.BobMessage.MightyFineLiquer:
                    return "That's mighty fine sippin' liquer";
                case MessageType.BobMessage.Tipsy:
                    return "Leaving the saloon, feelin' good";
                case MessageType.BobMessage.ReadyToWork:
                    IsSleeping = false;
                    return "All mah fatigue has drained away. Time to find more gold!";
                case MessageType.BobMessage.GoingToMine:
                    return "Walkin' to the goldmine";
                case MessageType.BobMessage.Sleeping:
                    IsSleeping = true;
                    return "ZZZZzzzzzzzZZZzzzz...";
                case MessageType.BobMessage.PickUpNugget:
                    return "Wohooo a mighty fine gold nugget!";
                case MessageType.BobMessage.LeavingMine:
                    return "Ah'm leavin' the goldmine with mah pockets full o' sweet gold";
                case MessageType.BobMessage.GoingToBank:
                    return "Goin' to the bank. Yes siree";
                case MessageType.BobMessage.DepositAtBank:
                    return "Depositing gold. Total savings now:" + MIMoneyInBank;
                case MessageType.BobMessage.Rich:
                    return "WooHoo! Rich enough for now. Back home to mah li'lle lady";
                case MessageType.BobMessage.LeaveBank:
                    return "Leavin' the bank";
                case MessageType.BobMessage.LetsDrinkMaster:
                    return "Master, let go drink some whisssskyyyyy";
                case MessageType.BobMessage.Thirsty:
                    return "Sooo thirsty, time to go drink";
                case MessageType.BobMessage.LetsTrainMaster:
                    return "Master, time to tra -hic- innn";
                case MessageType.BobMessage.LeavingSaloon:
                    return "Time to leave the saloon!";
                case MessageType.BobMessage.Asskicked:
                    return "Got my butt kicked by Master again! Damn!";
                case MessageType.BobMessage.LeavingShack:
                    return "Have a good day sweetheart.";
                default:
                    return "Invalid message, couldn't find the string";
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            if (elsa != null)
                elsa.OnWifeyMessageSentToMiner += ReactToMessage;
            StartCoroutine(UpdateInBackground());
        }

        // Update is called once per frame
        void ReactToMessage(MessageType.ElsaMessage elsaMessageReceived)
        {
            
        }

        public bool          PocketsFull(){return MIGoldCarried >= MaxNuggets;}

        public bool Fatigued()
        {
            if (!IsSleeping)
                return MIFatigue > TirednessThreshold; 
            return MIFatigue == 0;
        }
        public void          DecreaseFatigue(){MIFatigue = 0;}
        public void          IncreaseFatigue(){MIFatigue += 1;}

        public int           Wealth(){return MIMoneyInBank;}

        public void AddToWealth(int val)
        {
            MIMoneyInBank += val;
        }

        public bool Thirsty()
        {
            return MIThirst > ThirstLevel;
        }
        public void          BuyAndDrinkAWhiskey(){MIThirst = 0; MIMoneyInBank-=2;}

        public IEnumerator UpdateInBackground()
        {
             yield return new WaitForSeconds(1);
             MIThirst += 1;

             if (IsSleeping)
             {
                 DecreaseFatigue();
                 if (MIFatigue <= 0)
                 {
                     MIFatigue = 0;
                     IsSleeping = false;
                 }
             }
             else
                 IncreaseFatigue();
             
             stateMachine.Update();
             if(continueUpdate)
                StartCoroutine(UpdateInBackground());
        }
    }
}

public delegate void MinerMessage(MessageType.BobMessage bobMessageSent);




