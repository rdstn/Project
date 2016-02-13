using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AuthoredGuestManager : EventManager
{

    public float affairChance;
    public float affairLimit;
    public float newAffairChance;
    public float confessAffairChance;
    public float gossipChance;
    public int character;

    public float waitTime;

    public GameObject receptionist;

    public GameObject SO;

    public GameObject[] otherGuests;

    public GameObject[] toilet;
    public GameObject[] snooker;
    public GameObject[] restaurant;
    public GameObject[] drinksTables;
    public GameObject[] life;

    public GameObject waitingRoom;
    public GameObject bar;
    public GameObject stairwell;
    public GameObject reception;

    private GameObject hotelRoom; //CHEAT

    private bool hadAffair;

    private bool confessed = false;
    //private bool panic = false;

    private GameObject carrier;
    private GameObject log;
    public double[] priorities_actual; //Used to pick behaviour trees to add to the queue.
    public string[] traits; //Used to store the traits in a more convenient way.
    public int[,] priority_defines; //Used to read author-assigned priorities.
    public int[] relationships; //Likewise, for relationships.
    public string actor_name; //Likewise, for name.

    protected override void Awake()
    {
        base.Awake();
        carrier = GameObject.Find("Carrier");
        log = GameObject.Find("Log");
        priorities_actual = new double[7];
        if (character == 1)
        {
            priority_defines = carrier.GetComponent<CarrierScript>().red1priorities;
            relationships = carrier.GetComponent<CarrierScript>().red1relationships;
            traits = carrier.GetComponent<CarrierScript>().red1traits;
            actor_name = carrier.GetComponent<CarrierScript>().red1name;
        }
        else if (character == 2)
        {
            priority_defines = carrier.GetComponent<CarrierScript>().red2priorities;
            relationships = carrier.GetComponent<CarrierScript>().red2relationships;
            traits = carrier.GetComponent<CarrierScript>().red2traits;
            actor_name = carrier.GetComponent<CarrierScript>().red2name;
        }
        else if (character == 3)
        {
            priority_defines = carrier.GetComponent<CarrierScript>().green1priorities;
            relationships = carrier.GetComponent<CarrierScript>().green1relationships;
            traits = carrier.GetComponent<CarrierScript>().green1traits;
            actor_name = carrier.GetComponent<CarrierScript>().green1name;
        }
        else if (character == 4)
        {
            priority_defines = carrier.GetComponent<CarrierScript>().green2priorities;
            relationships = carrier.GetComponent<CarrierScript>().green2relationships;
            traits = carrier.GetComponent<CarrierScript>().green2traits;
            actor_name = carrier.GetComponent<CarrierScript>().green2name;
        }
        else if (character == 5)
        {
            priority_defines = carrier.GetComponent<CarrierScript>().blue1priorities;
            relationships = carrier.GetComponent<CarrierScript>().blue1relationships;
            traits = carrier.GetComponent<CarrierScript>().blue1traits;
            actor_name = carrier.GetComponent<CarrierScript>().blue1name;
        }
        else if (character == 6)
        {
            priority_defines = carrier.GetComponent<CarrierScript>().blue2priorities;
            relationships = carrier.GetComponent<CarrierScript>().blue2relationships;
            traits = carrier.GetComponent<CarrierScript>().blue2traits;
            actor_name = carrier.GetComponent<CarrierScript>().blue2name;
        }
        else
        {
            Debug.Log("Adding character data to unknown character, numbered " + character);
        }
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        AddTree(new WaitTree(this, 2), 6);
        AddTree(new FindRoom(this, "receptionist", receptionist, reception), 8);
        AddTree(new WaitTree(this, waitTime), 50);
        //AddTree(new SecretRomance(this, "receptionist", receptionist), 11);

        //Follows priority assignments.

        //Tree 1, toilet visiting. Naturally the least likely tree. (mult of 1)
        //The more outgoing a person is, the more likely they are to try and spend more time around others.
        double trait_score = 0; //Trait influence
        if (Array.IndexOf<string>(traits, "Lazy") != -1 || Array.IndexOf<string>(traits, "Shy") != -1)
        {
            trait_score = trait_score + 0.5;
        }
        if (Array.IndexOf<string>(traits, "Energetic") != -1 || Array.IndexOf<string>(traits, "Outgoing") != -1)
        {
            trait_score = trait_score - 0.5;
        }
        priorities_actual[0] = 1 * priority_defines[0, 0] / 3 + trait_score;
        if (priorities_actual[0]<0)
        {
            priorities_actual[0] = 0;
        }

        //Tree 2, watching TV. Likewise as tree 1.
        trait_score = 0; //Trait influence
        if (Array.IndexOf<string>(traits, "Lazy") != -1 || Array.IndexOf<string>(traits, "Shy") != -1)
        {
            trait_score = trait_score + 0.5;
        }
        if (Array.IndexOf<string>(traits, "Energetic") != -1 || Array.IndexOf<string>(traits, "Outgoing") != -1)
        {
            trait_score = trait_score - 0.5;
        }
        priorities_actual[1] = 2 * priority_defines[1, 0] / 3 + trait_score;
        if (priorities_actual[1] < 0)
        {
            priorities_actual[1] = 0;
        }

        //Tree 3, "chilling". Likewise as the previous trees.
        trait_score = 0; //Trait influence
        if (Array.IndexOf<string>(traits, "Lazy") != -1 || Array.IndexOf<string>(traits, "Shy") != -1)
        {
            trait_score = trait_score + 0.5;
        }
        if (Array.IndexOf<string>(traits, "Energetic") != -1 || Array.IndexOf<string>(traits, "Outgoing") != -1)
        {
            trait_score = trait_score - 0.5;
        }
        priorities_actual[2] = 1.5 * priority_defines[2, 0] / 3 + trait_score;
        if (priorities_actual[2] < 0)
        {
            priorities_actual[2] = 0;
        }

        //Tree 4, playing pool. Opposite to previous trees. 
        //We refer to the neutral priority [x,0] only since we don't have meaningful difference between friends/enemies yet.
        trait_score = 0; //Trait influence
        if (Array.IndexOf<string>(traits, "Lazy") != -1 || Array.IndexOf<string>(traits, "Shy") != -1)
        {
            trait_score = trait_score - 0.5;
        }
        if (Array.IndexOf<string>(traits, "Energetic") != -1 || Array.IndexOf<string>(traits, "Outgoing") != -1)
        {
            trait_score = trait_score + 0.5;
        }
        priorities_actual[3] = 2 * priority_defines[3, 0] / 3 + trait_score;
        if (priorities_actual[3] < 0)
        {
            priorities_actual[3] = 0;
        }

        //Tree 5, eating at the restaurant. Outgoing, Lazy and Gluttonous push it up, opposite traits lower chance.
        trait_score = 0; //Trait influence
        if (Array.IndexOf<string>(traits, "Lazy") != -1 || Array.IndexOf<string>(traits, "Outgoing") != -1)
        {
            trait_score = trait_score + 0.5;
        }
        if (Array.IndexOf<string>(traits, "Gluttonous") != -1)
        {
            trait_score = trait_score + 0.5;
        }
        if (Array.IndexOf<string>(traits, "Energetic") != -1 || Array.IndexOf<string>(traits, "Shy") != -1)
        {
            trait_score = trait_score - 0.5;
        }
        if (Array.IndexOf<string>(traits, "Temperate") != -1)
        {
            trait_score = trait_score - 0.5;
        }
        priorities_actual[4] = 2 * priority_defines[4, 0] / 3 + trait_score;
        if (priorities_actual[4] < 0)
        {
            priorities_actual[4] = 0;
        }

        //Tree 6, drinking. Likewise, only a bit stronger trait swings.
        trait_score = 0; //Trait influence
        if (Array.IndexOf<string>(traits, "Lazy") != -1 || Array.IndexOf<string>(traits, "Outgoing") != -1)
        {
            trait_score = trait_score + 0.75;
        }
        if (Array.IndexOf<string>(traits, "Gluttonous") != -1)
        {
            trait_score = trait_score + 0.75;
        }
        if (Array.IndexOf<string>(traits, "Energetic") != -1 || Array.IndexOf<string>(traits, "Shy") != -1)
        {
            trait_score = trait_score - 0.75;
        }
        if (Array.IndexOf<string>(traits, "Temperate") != -1)
        {
            trait_score = trait_score - 0.75;
        }
        priorities_actual[5] = 3 * priority_defines[5, 0] / 3 + trait_score;
        if (priorities_actual[5] < 0)
        {
            priorities_actual[5] = 0;
        }

        //Tree 7, "love". Influenced by otugoing and amorous. Energetic is also a small bonus/malus.
        trait_score = 0; //Trait influence
        if (Array.IndexOf<string>(traits, "Amorous") != -1 || Array.IndexOf<string>(traits, "Outgoing") != -1)
        {
            trait_score = trait_score + 1.5;
        }
        if (Array.IndexOf<string>(traits, "Energetic") != -1)
        {
            trait_score = trait_score + 0.5;
        }
        if (Array.IndexOf<string>(traits, "Chaste") != -1 || Array.IndexOf<string>(traits, "Shy") != -1)
        {
            trait_score = trait_score - 0.75;
        }
        if (Array.IndexOf<string>(traits, "Lazy") != -1)
        {
            trait_score = trait_score - 0.25;
        }
        priorities_actual[6] = 1 * priority_defines[6, 0] / 3 + trait_score;
        if (priorities_actual[6] < 0)
        {
            priorities_actual[6] = 0;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void AcceptKnowledge(Sentence sentence)
    {
        if (!knowledgeBase.ContainsSentence(sentence))
        {
            if (sentence.noun1 == this.gameObject)
            {
                if (sentence.verb == Sentence.Verb.StayingIn)
                {
                    hotelRoom = sentence.noun2; //CHEAT, can look in knowledgebase to find out
                    AddTree(new DropBags(this, "room", sentence.noun2), 7);
                }
            }

            if (sentence.verb == Sentence.Verb.Love)
            {
                if (sentence.noun1 == SO || sentence.noun2 == SO)
                {
                    if (sentence.noun1 != gameObject && sentence.noun2 != gameObject)
                    {
                        if (sentence.noun1 != SO)
                        {
                            AddTree(new StalkAndKill(this, "target", sentence.noun1), 30);
                        }
                        else
                        {
                            AddTree(new StalkAndKill(this, "target", sentence.noun2), 30);
                        }
                    }
                }
            }

            if (sentence.verb == Sentence.Verb.Murder && sentence.noun1 != gameObject)
            {
                if (!knowledgeBase.Murdered(gameObject, sentence.noun2))
                {
                    AddTree(new FoundCorpse(this, receptionist, sentence), 20);
                }
            }
        }
        base.AcceptKnowledge(sentence);
    }

    public override void TreeCompleted(bool success, BehaviourTree tree)
    {
        base.TreeCompleted(success, tree);

        if (behaviourQueue.Count == 0)
        {
            AddRandomFluffTree();
        }

        //Consider new affair
        float randval = UnityEngine.Random.value;
        if (randval < priorities_actual[6] / 10)
        {
            GameObject romanceTarget;
            //Debug.Log("Character " + character + "Who has a priority for love " + priorities_actual[6]/10 + ", is now in affair, because of randval. " + randval);

            if ((knowledgeBase.Affairs(gameObject, SO) >= affairLimit || UnityEngine.Random.value > newAffairChance) && knowledgeBase.Affairs(gameObject, SO) >= 1)
            {
                //Use current target
                romanceTarget = knowledgeBase.RandomAffair(gameObject, SO);

            }
            else
            {
                do
                {
                    romanceTarget = RandomFromArray(otherGuests);
                } while (romanceTarget == SO);
            }

            AddTree(new SecretRomance(this, "romanceTarget", romanceTarget), 15);
        }

        //Confess?
        if (!confessed && knowledgeBase.Affairs(gameObject, SO) >= 1 && UnityEngine.Random.value < confessAffairChance)
        {
            confessed = true;
            AddTree(new ConfessAffair(this, knowledgeBase.RandomAffair(gameObject, SO)), 19);
        }

        if (UnityEngine.Random.value < gossipChance)
        {
            AddGossip();
        }
    }

    private void AddRandomFluffTree()
    {
        double prioritytotal = 0;
        double prioritystep = 0; //We need this to properly do priorities.
        for (int i = 0; i < priorities_actual.Length - 1; i++)
        {
            prioritytotal += priorities_actual[i];
        }
        float randNumber = UnityEngine.Random.Range(0, (float)prioritytotal);
        if (randNumber < priorities_actual[0])
        {
            AddTree(new Toilet(this, 5, 10, RandomFromArray(toilet)), 5);
        }
        var priotemp = prioritystep;
        prioritystep += priorities_actual[0];
        if (randNumber < prioritystep+priorities_actual[1] && randNumber >= priotemp)
        {
            AddTree(new TV(this, 20, 30, hotelRoom), 2);
        }
        priotemp = prioritystep;
        prioritystep += priorities_actual[1];
        if (randNumber < prioritystep+priorities_actual[2] && randNumber >= priotemp)
        {
            AddTree(new Chill(this, 20, 30, hotelRoom), 2);
        }
        priotemp = prioritystep;
        prioritystep += priorities_actual[2];
        if (randNumber < prioritystep+priorities_actual[3] && randNumber >= priotemp)
        {
            var partner = RandomFromArray(otherGuests);
            AddTree(new Snooker(this, RandomFromArray(snooker), partner), 4);
        }
        priotemp = prioritystep;
        prioritystep += priorities_actual[3];
        if (randNumber < prioritystep+priorities_actual[4] && randNumber >= priotemp)
        {
            AddTree(new Meal(this, 20, 30, RandomFromArray(restaurant)), 5);
        }
        priotemp = prioritystep;
        prioritystep += priorities_actual[4];
        if (randNumber < prioritystep+priorities_actual[5] && randNumber >= priotemp)
        {
            AddTree(new Bar(this, 10, 20, bar), 2);
        }
        else
        {
            Debug.Log("Tryig to add a 'fluff' priority tree which is out of bounds." + randNumber + "Bound is: " + prioritytotal);
        }
    }

    private void AddGossip()
    {
        Sentence gossip = knowledgeBase.RandomGossip();
        if (gossip != null)
        {
            GameObject target;
            do
            {
                target = RandomFromArray(otherGuests);
            } while (target == gossip.noun1 || target == gossip.noun2);
            AddTree(new Gossip(this, target, gossip), 13);
        }
    }

    private GameObject RandomFromArray(GameObject[] array)
    {
        return array[UnityEngine.Random.Range(0, array.Length)];
    }
}
