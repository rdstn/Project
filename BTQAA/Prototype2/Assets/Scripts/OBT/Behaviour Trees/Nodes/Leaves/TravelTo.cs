using UnityEngine;
using System.Collections;

public class TravelTo : Leaf_Wait {

	private string destinationKey;
	private Vector3 destination;
	private bool destinationSet = false;

	public TravelTo(BehaviourTree tree, string destinationKey) : base(tree){
		this.destinationKey = destinationKey;
	}

	public TravelTo(BehaviourTree tree, Vector3 destination) : base(tree){
		this.destination = destination;
		destinationSet = true;
	}

	public override void Poke(Parent parent){
		base.Poke(parent);
	
		if(destinationSet == false){
			destination = tree.GetFromBlackboard<Vector3>(destinationKey);
		}

		if(destination == default(Vector3)){
			parent.ReceiveStatus(Status.Failure);
		}
		else{
			//Debug.Log (tree.eventManager.gameObject);
			//Debug.Log (destination);
			tree.actions.TravelTo(destination, this); 
		}
	}

	public override void ReceiveStatus (Status status)
	{
		if(status == Status.Failure){
		}
		base.parent.ReceiveStatus(status);
	}

	public override void Interrupt ()
	{
		tree.actions.TravelToInterrupt();
	}
}
