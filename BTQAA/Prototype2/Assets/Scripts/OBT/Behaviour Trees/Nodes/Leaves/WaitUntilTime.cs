using UnityEngine;
using System.Collections;

public class WaitUntilTime : Leaf_Wait {
	
	float time;
	
	public WaitUntilTime(BehaviourTree tree, float time) : base(tree){
		this.time = time;
	}
	
	public override void Poke (Parent parent)
	{
		base.Poke (parent);
		tree.actions.WaitUntilTime(time, this);
		
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
