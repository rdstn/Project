using UnityEngine;
using System.Collections;

public class Wait : Leaf_Wait {

	float time;
	float minTime;
	float maxTime;
	bool randomTime = false;

	public Wait(BehaviourTree tree, float time) : base(tree){
		this.time = time;
	}

	public Wait(BehaviourTree tree, float minTime, float maxTime) : base(tree){
		randomTime = true;
		this.minTime = minTime;
		this.maxTime = maxTime;
	}

	public override void Poke (Parent parent)
	{
		base.Poke (parent);
		if(!randomTime){
			tree.actions.Wait(time, this);
		}
		else{
			tree.actions.Wait(Random.Range(minTime, maxTime), this);
		}

	}

	public override void ReceiveStatus (Status status)
	{
		parent.ReceiveStatus(status);
	}

	public override void Interrupt ()
	{
		tree.actions.WaitInterrupt();
	}
}
