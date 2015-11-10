using UnityEngine;
using System.Collections;

public class SetSpeed : Leaf_Instant {
	
	float speed;
	
	public SetSpeed(BehaviourTree tree, float speed) : base(tree){
		this.speed = speed;
	}
	
	public override void Poke (Parent parent)
	{
		base.Poke (parent);

		tree.actions.SetSpeed(speed);
		parent.ReceiveStatus(Status.Success);
	}
}
