using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GuestManager : EventManager {

	public float affairChance;
	public float affairLimit;
	public float newAffairChance;
	public float confessAffairChance;
	public float gossipChance;
    //Lists all traits. Can be edited directly via editor, or modified via Authoring GUI in-game.
    //Traits act as modifiers on the likelihood and/or outcomes of other behaviour trees.
    public string[] traits;
    //Lists all priorities. Those act as modifiers on the likelihood of adding other behaviour trees.
    public float[] priorities = new float[10];

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

	protected override void Awake ()
	{
		base.Awake ();
	}

	// Use this for initialization
	protected override void Start () {
		base.Start();
		AddTree(new WaitTree(this, 2), 6);
		AddTree(new FindRoom(this, "receptionist", receptionist, reception), 8);
		AddTree(new WaitTree(this, waitTime), 50);
		//AddTree(new SecretRomance(this, "receptionist", receptionist), 11);
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	}

	public override void AcceptKnowledge (Sentence sentence)
	{
		if(!knowledgeBase.ContainsSentence(sentence)){
			if(sentence.noun1 == this.gameObject){
				if(sentence.verb == Sentence.Verb.StayingIn){
					hotelRoom = sentence.noun2; //CHEAT, can look in knowledgebase to find out
					AddTree(new DropBags(this, "room", sentence.noun2), 7);
				}
			}

			if(sentence.verb == Sentence.Verb.Love){
				if(sentence.noun1 == SO || sentence.noun2 == SO){
					if(sentence.noun1 != gameObject && sentence.noun2 != gameObject){
						if(sentence.noun1 != SO){
							AddTree(new StalkAndKill(this, "target", sentence.noun1), 30);
						}
						else{
							AddTree(new StalkAndKill(this, "target", sentence.noun2), 30);
						}
					}
				}
			}

			if(sentence.verb == Sentence.Verb.Murder && sentence.noun1 != gameObject){
				if(!knowledgeBase.Murdered(gameObject, sentence.noun2)){
					AddTree(new FoundCorpse(this, receptionist, sentence), 20);
				}
			}
		}
		base.AcceptKnowledge (sentence);
	}

	public override void TreeCompleted (bool success, BehaviourTree tree)
	{
		base.TreeCompleted (success, tree);

		if(behaviourQueue.Count == 0){
			AddRandomFluffTree();
		}

		//Consider new affair
		if(Random.value < affairChance){
			GameObject romanceTarget;

			if((knowledgeBase.Affairs(gameObject, SO) >= affairLimit || Random.value > newAffairChance) && knowledgeBase.Affairs(gameObject, SO) >= 1){
				//Use current target
				romanceTarget = knowledgeBase.RandomAffair(gameObject, SO);

			}
			else{
				do{
					romanceTarget = RandomFromArray(otherGuests);
				}while (romanceTarget == SO);
			}

			AddTree(new SecretRomance(this, "romanceTarget", romanceTarget), 15);
		}

		//Confess?
		if(!confessed && knowledgeBase.Affairs(gameObject, SO) >= 1 && Random.value < confessAffairChance){
			confessed = true;
			AddTree(new ConfessAffair(this, knowledgeBase.RandomAffair(gameObject, SO)), 19);
		}

		if(Random.value < gossipChance){
			AddGossip();
		}
	}

	private void AddRandomFluffTree(){
		int randNumber = Random.Range(0, 20);
		if(randNumber < 1){
			AddTree(new Toilet(this, 5, 10, RandomFromArray(toilet)), 5);
		}
		else if(randNumber < 3){
			AddTree(new TV(this, 20, 30, hotelRoom), 2);
		}
		else if(randNumber < 5){
			AddTree(new Chill(this, 20, 30, hotelRoom), 2);
		}
		else if(randNumber < 11){
			AddTree(new Snooker(this, RandomFromArray(snooker), RandomFromArray(otherGuests)), 4);
		}
		else if(randNumber < 12){
			AddTree(new Meal(this, 20, 30, RandomFromArray(restaurant)), 5);
		}
		else{
			AddTree(new Bar(this, 10, 20, bar), 2);
		}
	}

	private void AddGossip(){
		Sentence gossip = knowledgeBase.RandomGossip();
		if(gossip != null){
			GameObject target;
			do{
				target = RandomFromArray(otherGuests);
			} while (target == gossip.noun1 || target == gossip.noun2);
			AddTree(new Gossip(this, target, gossip), 13);
		}
	}

	private GameObject RandomFromArray(GameObject[] array){
		return array[Random.Range(0, array.Length)];
	}
}
