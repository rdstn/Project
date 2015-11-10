using UnityEngine;
using System.Collections;

public class GenericManager : EventManager {

	// Use this for initialization
	protected override void Start () {
		base.Start();

		AddTree(NewRandomDestinationTree());
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	}

	public override void TreeCompleted (bool success, BehaviourTree tree)
	{
		base.TreeCompleted(success, tree);

		if(tree is SimpleRandomMove){
			AddTree(NewRandomDestinationTree());
		}
	}

	private PrioritisedBehaviour NewRandomDestinationTree(){
		PrioritisedBehaviour randDest = new PrioritisedBehaviour();
		randDest.behaviourTree = new SimpleRandomMove(this);
		randDest.priority = 1;
		return randDest;
	}
}
