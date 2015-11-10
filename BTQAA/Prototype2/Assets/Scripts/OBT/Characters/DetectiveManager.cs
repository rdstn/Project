using UnityEngine;
using System.Collections;

public class DetectiveManager : EventManager {

	public GameObject receptionist;

	public GameObject jail;

	protected override void Awake ()
	{
		base.Awake ();
	}

	protected override void Start ()
	{
		base.Start ();
	}

	protected override void Update ()
	{
		base.Update ();
	}

	public override void TreeCompleted (bool success, BehaviourTree tree)
	{
		base.TreeCompleted (success, tree);
	}

	public override void AcceptKnowledge (Sentence sentence)
	{
		if(!knowledgeBase.ContainsSentence(sentence)){
			if(sentence.verb == Sentence.Verb.Murder){
				AddTree(new Investigate(this, sentence.noun2, receptionist, jail) ,17);
			}
		}
		base.AcceptKnowledge (sentence);
	}
}
