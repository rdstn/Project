﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
    public List<GameObject> friends;
    public List<GameObject> enemies;

    public GameObject[] toilet;
    public GameObject[] snooker;
    public GameObject[] restaurant;
    public GameObject[] drinksTables;
    public GameObject[] life;

    public GameObject waitingRoom;
    public GameObject bar;
    public GameObject stairwell;
    public GameObject reception;
    public GameObject[] rooms;

    private GameObject hotelRoom; //CHEAT

    private bool hadAffair;

    private bool confessed = false;
    //private bool panic = false;

    private GameObject carrier;
    private GameObject log;
    public double[] priorities_actual; //Used to pick behaviour trees to add to the queue.
    public double[] priorities_friends;
    public double[] priorities_enemies;
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
        priorities_friends = new double[7];
        priorities_enemies = new double[7];
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

        //Get friends and enemies.
        if (relationships[0]==1 && character!=1)
        {
            friends.Add(GameObject.Find("red_male"));
        }
        else if (relationships[0]==2 && character != 1)
        {
            enemies.Add(GameObject.Find("red_male"));
        }
        if (relationships[1] == 1 && character != 2)
        {
            friends.Add(GameObject.Find("red_female"));
        }
        else if (relationships[1] == 2 && character != 2)
        {
            enemies.Add(GameObject.Find("red_female"));
        }
        if (relationships[2] == 1 && character != 3)
        {
            friends.Add (GameObject.Find("green_male"));
        }
        else if (relationships[2] == 2 && character != 3)
        {
            enemies.Add(GameObject.Find("green_male"));
        }
        if (relationships[3] == 1 && character != 4)
        {
            friends.Add(GameObject.Find("green_female"));
        }
        else if (relationships[3] == 2 && character != 4)
        {
            enemies.Add(GameObject.Find("green_female"));
        }
        if (relationships[4] == 1 && character != 5)
        {
            friends.Add(GameObject.Find("blue_male"));
        }
        else if (relationships[4] == 2 && character != 5)
        {
            enemies.Add(GameObject.Find("blue_male"));
        }
        if (relationships[5] == 1 && character != 6)
        {
            friends.Add(GameObject.Find("blue_female"));
        }
        else if (relationships[5] == 2 && character != 6)
        {
            enemies.Add(GameObject.Find("blue_female"));
        }
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        //AddTree(new WaitTree(this, 2), 6);
        AddTree(new FindRoom(this, "receptionist", receptionist, reception), 8);
        //AddTree(new WaitTree(this, waitTime), 50);
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
        priorities_actual[0] = 1.00 * (double)priority_defines[0, 0] / 3.00 + trait_score;
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
        priorities_actual[1] = 2.00 * (double)priority_defines[1, 0] / 3.00 + trait_score;
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
        priorities_actual[2] = 1.5 * priority_defines[2, 0] / 3.00 + trait_score;
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
        priorities_actual[3] = 2.00 * (double)priority_defines[3, 0] / 3.00 + trait_score;
        priorities_friends[3] = 2.00 * (double)priority_defines[3, 2] / 3.00 + trait_score;
        priorities_enemies[3] = 2.00 * (double)priority_defines[3, 1] / 3.00 + trait_score;
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
        priorities_actual[4] = 2.00 * (double)priority_defines[4, 0] / 3.00 + trait_score;
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
        priorities_actual[5] = 3.00 * (double)priority_defines[5, 0] / 3.00 + trait_score;
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
        priorities_actual[6] = 1.00 * (double)priority_defines[6, 0] / 3.00 + trait_score;
        priorities_friends[6] = 1.00 * (double)priority_defines[6, 2] / 3.00 + trait_score;
        priorities_enemies[6] = 1.00 * (double)priority_defines[6, 1] / 3.00 + trait_score;
        //print(Array.IndexOf<string>(traits, "Amorous"));
        //print(priority_defines[6, 0] + " " + priorities_actual[6] + " " + actor_name);
        if (priorities_actual[6] < 0)
        {
            priorities_actual[6] = 0;
        }

        //Gossip chance - outgoing people have it doubled, shy halved, honorable set to 0.
        if (traits.Contains("Outgoing")) { gossipChance = gossipChance * 2; }
        if (traits.Contains("Shy")) { gossipChance = gossipChance / 2; }
        if (traits.Contains("Honorable")) { gossipChance = 0; }

        //Confess affair chance - paranoid people have it halved, trusting have it doubled, honorable quadrupled, cruel set to 0.
        if (traits.Contains("Paranoid")) { confessAffairChance = confessAffairChance / 2; }
        if (traits.Contains("Trusting")) { confessAffairChance = confessAffairChance * 2; }
        if (traits.Contains("Honorable")) { confessAffairChance = confessAffairChance * 4; }
        if (traits.Contains("Cruel")) { confessAffairChance = 0; }
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

            //Murder. Kind/honorable actors will not do it, everyone else will.
            if (sentence.verb == Sentence.Verb.Love)
            {
                if (sentence.noun1 == SO || sentence.noun2 == SO)
                {
                    if (sentence.noun1 != gameObject && sentence.noun2 != gameObject)
                    {
                        if (sentence.noun1 != SO && (!traits.Contains("Kind") || !traits.Contains("Honorable")))
                        {
                            AddTree(new StalkAndKill(this, "target", sentence.noun1), 30);
                        }
                        else if (!traits.Contains("Kind") && !traits.Contains("Honorable"))
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
                    AddTree(new Lockup(this, 400, RandomFromArray(rooms)), 19);
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
                //If we have at least two friends and we prefer having affairs with friends, go for friends.
                if (friends.Count!=0 && priorities_friends[6]>priorities_enemies[6] && priorities_friends[6]>priorities_actual[6])
                {
                    int index = UnityEngine.Random.Range(0, friends.Count - 1);
                    romanceTarget = friends[index];
                }

                else if (enemies.Count!=0 && priorities_enemies[6] > priorities_friends[6] && priorities_enemies[6] > priorities_actual[6])
                {
                    int index = UnityEngine.Random.Range(0, enemies.Count - 1);
                    romanceTarget = enemies[index];
                   // enemies.Remove(romanceTarget);
                }
                else
                {
                    romanceTarget = RandomFromArray(otherGuests);
                }

                int chance = UnityEngine.Random.Range(0, 2);
                string[] targetsTraits = romanceTarget.GetComponentInChildren<AuthoredGuestManager>().traits;

                //No luck, target is chaste and honorable
              /*  if (targetsTraits.Contains("Chaste") && targetsTraits.Contains("Honorable"))
                {
                    AddTree(new Fail(this, 20, romanceTarget), 15);
                    AddTree(new Lockup(this, 20, RandomFromArray(rooms)), 14);
                }

                //We might do it, the target is chaste OR honorable.
                else if (targetsTraits.Contains("Chaste") || targetsTraits.Contains("Honorable"))
                {
                    if (chance == 0)
                    {
                        AddTree(new SecretRomance(this, "romanceTarget", romanceTarget), 15);
                    }
                    else
                    {
                        AddTree(new Fail(this, 10, romanceTarget), 15);
                        AddTree(new Lockup(this, 20, RandomFromArray(rooms)), 14);
                    }
                }

                //Target is neither particularly chaste, nor honorable.
                else
                {
                    AddTree(new SecretRomance(this, "romanceTarget", romanceTarget), 15);
                }
				*/
				AddTree(new SecretRomance(this, "romanceTarget", romanceTarget), 15);

            }

            //In case we get stuck, do a bit of random movement.
            AddTree(new SimpleRandomMove(this), 14);
            
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
        //"Test" is used as a test actor, to look at behaviour of the code.
        double prioritytotal = 0;
        double prioritystep = 0; //We need this to properly do priorities.
        double randNumber = 0;
        double priotemp = 0;
        for (int i = 0; i < priorities_actual.Length - 1; i++)
        {
            prioritytotal += priorities_actual[i];
        }
        randNumber = UnityEngine.Random.Range(0.00f, (float)prioritytotal);
        if (actor_name == "Test") { print(randNumber); }
        if (randNumber < priorities_actual[0])
        {
            AddTree(new Toilet(this, 5, 10, RandomFromArray(toilet)), 1);
        }
        priotemp = prioritystep+priorities_actual[0];
        prioritystep += priorities_actual[0];
        if (actor_name == "Test") { print("Step = " + prioritystep + ". Temp is = " + priotemp); }
        if (randNumber < prioritystep+priorities_actual[1] && randNumber >= priotemp)
        {
            AddTree(new TV(this, 20, 30, hotelRoom), 2);
        }
        priotemp = prioritystep+priorities_actual[1];
        prioritystep += priorities_actual[1];
        if (actor_name == "Test") { print("Step = " +prioritystep + ". Temp is = " + priotemp); }
        if (randNumber < prioritystep+priorities_actual[2] && randNumber >= priotemp)
        {
            AddTree(new Chill(this, 20, 30, hotelRoom), 2);
        }
        priotemp = prioritystep+priorities_actual[2];
        prioritystep += priorities_actual[2];
        if (actor_name == "Test") { print("Step = " + prioritystep + ". Temp is = " + priotemp); }
        if (randNumber < prioritystep+priorities_actual[3] && randNumber >= priotemp)
        {
            GameObject partner = null;
            //If we prefer playing with friends, good, do so.
            if (friends.Count!=0 && priorities_friends[3]>=priorities_enemies[3] && priorities_friends[3]>=priorities_actual[3])
            {
                int index = UnityEngine.Random.Range(0, friends.Count);
                partner = friends[index];
                AddTree(new Snooker(this, RandomFromArray(snooker), partner), 4);
            }
            //If we prefer playing with enemies.
            else if (enemies.Count!=0 && priorities_enemies[3] >= priorities_friends[3] && priorities_enemies[3] >= priorities_actual[3])
            {
                int index = UnityEngine.Random.Range(0, enemies.Count);
                partner = enemies[index];
                int success = 0;
                //If the enemy is unkind, the interaction has 50% chance to backfire and lead to lockup time.
                if (!partner.GetComponentInChildren<AuthoredGuestManager>().traits.Contains("Kind"))
                {
                    success = UnityEngine.Random.Range(0, 2);
                }
                if (success==0)
                {
                    enemies.Remove(partner);
                    AddTree(new Snooker(this, RandomFromArray(snooker), partner), 4);
                }
                else {
                    AddTree(new Fail(this, 10, partner),3);
                    AddTree (new Lockup(this, 20, RandomFromArray(rooms)),2);
                }
            }
            //If we don't care, friends or enemies.
            else
            {
                partner = RandomFromArray(otherGuests);
                AddTree(new Snooker(this, RandomFromArray(snooker), partner), 4);
            }
        }
        priotemp = prioritystep+priorities_actual[3];
        prioritystep += priorities_actual[3];
        if (actor_name == "Test") { print("Step = " + prioritystep + ". Temp is = " + priotemp); }
        if (randNumber < prioritystep+priorities_actual[4] && randNumber >= priotemp)
        {
            AddTree(new Meal(this, 20, 30, RandomFromArray(restaurant)), 5);
        }
        priotemp = prioritystep+priorities_actual[4];
        prioritystep += priorities_actual[4];
        if (actor_name == "Test") { print("Step = " + prioritystep + ". Temp is = " + priotemp); }
        if (randNumber < prioritystep+priorities_actual[5] && randNumber >= priotemp)
        {
            AddTree(new Bar(this, 10, 20, bar), 2);
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
                if (friends.Count != 0)
                {
                    int index = UnityEngine.Random.Range(0, friends.Count);
                    target = friends[index];
                    //If the target is honorable, remove friendship. If no friendship, add enemity.
                    if (target.GetComponentInChildren<AuthoredGuestManager>().traits.Contains("Honorable"))
                    {
                        if (target.GetComponentInChildren<AuthoredGuestManager>().friends.Contains(this.transform.parent.gameObject))
                        {
                            target.GetComponentInChildren<AuthoredGuestManager>().friends.Remove(this.transform.parent.gameObject);
                        }
                        else if (!target.GetComponentInChildren<AuthoredGuestManager>().friends.Contains(this.transform.parent.gameObject))
                        {
                            target.GetComponentInChildren<AuthoredGuestManager>().enemies.Add(this.transform.parent.gameObject);
                        }
                    }
                }
                else if (enemies.Count != 0)
                {
                    int index = UnityEngine.Random.Range(0, enemies.Count - 1);
                    target = enemies[index];
                    //If we interact with an enemy in a gossip, we no longer consider them an enemy.
                    enemies.Remove(target);
                }
                else
                {
                    target = RandomFromArray(otherGuests);
                }
            } while (target == gossip.noun1 || target == gossip.noun2);
            AddTree(new Gossip(this, target, gossip), 13);
        }
    }

    private GameObject RandomFromArray(GameObject[] array)
    {
        return array[UnityEngine.Random.Range(0, array.Length)];
    }
}
