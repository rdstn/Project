using UnityEngine;
using System.Collections;

public class ReceptionistManager : EventManager {

	public GameObject detective;

	public GameObject[] guests;

	public GameObject[] rooms;

	public GameObject reception;

	protected override void Awake ()
	{
		base.Awake ();
	}

	// Use this for initialization
	protected override void Start () {
		knowledgeBase.addSentence(new Sentence(guests[0], Sentence.Verb.StayingIn, rooms[0]));
		knowledgeBase.addSentence(new Sentence(guests[1], Sentence.Verb.StayingIn, rooms[0]));
		knowledgeBase.addSentence(new Sentence(guests[2], Sentence.Verb.StayingIn, rooms[1]));
		knowledgeBase.addSentence(new Sentence(guests[3], Sentence.Verb.StayingIn, rooms[1]));
		knowledgeBase.addSentence(new Sentence(guests[4], Sentence.Verb.StayingIn, rooms[2]));
		knowledgeBase.addSentence(new Sentence(guests[5], Sentence.Verb.StayingIn, rooms[2]));
		base.Start();

		AddTree(new ReceptionistIdle(this, 4, 30, reception), 1);
		//AddTree(new CallPolice(this, detective, new Sentence(null, Sentence.Verb.Murder,guests[1])), 50);
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	}

	public override void TreeCompleted (bool success, BehaviourTree tree)
	{
		base.TreeCompleted (success, tree);
		if(behaviourQueue.Count == 0){
			AddTree(new ReceptionistIdle(this, 4, 30, reception), 1);
		}
	}

	public override void AcceptKnowledge (Sentence sentence)
	{
		if(!knowledgeBase.ContainsSentence(sentence)){
			if(sentence.verb == Sentence.Verb.Murder){
				AddTree(new CallPolice(this, detective, sentence), 50);
			}
		}
		base.AcceptKnowledge (sentence);
	}
}
