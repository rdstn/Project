using UnityEngine;
using System.Collections;

public class Follow : Leaf_Wait {

	private string targetKey;
	private float stopping = 0.45f;

	
	public Follow(BehaviourTree tree, string targetKey) : base(tree){
		this.targetKey = targetKey;
	}

	public Follow(BehaviourTree tree, string targetKey, float stopping) : base(tree){
		this.targetKey = targetKey;
		this.stopping = stopping;
	}
	
	public override void Poke (Parent parent)
	{
		base.Poke (parent);
		GameObject target = tree.GetFromBlackboard<GameObject>(targetKey);
		if(target != null){
			tree.actions.Follow(target, this, stopping);
		}
		else{
			parent.ReceiveStatus(Status.Failure);
		}
	}

	public override void ReceiveStatus (Status status)
	{
		parent.ReceiveStatus(status);
	}

	public override void Interrupt ()
	{
		tree.actions.FollowInterrupt();
	}
}
