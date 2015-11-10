using UnityEngine;
using System.Collections;

public class MurdererManager : GuestManager {

	public GameObject wife;
	public GameObject target;

	protected override void Start () {
		base.Start();
		AddTree(new StalkAndKill(this, "target", target), 20);
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	}

	public override void TreeCompleted (bool success, BehaviourTree tree)
	{
		base.TreeCompleted (success, tree);
	}

	public override void AcceptKnowledge (Sentence sentence)
	{
		base.AcceptKnowledge (sentence);
		if(sentence.noun1 != this.gameObject && sentence.verb == Sentence.Verb.Love && sentence.noun2 == wife){
			AddTree(new StalkAndKill(this, "target", sentence.noun1), 20);
		}
	}
}
