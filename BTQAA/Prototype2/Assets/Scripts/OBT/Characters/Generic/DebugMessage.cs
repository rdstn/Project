using UnityEngine;
using System.Collections;

public class DebugMessage : Leaf_Instant {

	string message;

	public DebugMessage(BehaviourTree tree, string message) : base(tree){
		this.message = message;
	}

	public override void Poke (Parent parent)
	{
		base.Poke (parent);
		Debug.Log (tree.eventManager.gameObject);
		Debug.Log ("says");
		Debug.Log (message);
		parent.ReceiveStatus(Status.Success);
	}
}
