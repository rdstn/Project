using UnityEngine;
using System.Collections;

public class CheckInRange : Leaf_Instant {

	float range;
	string targetKey;

	public CheckInRange(BehaviourTree tree, string targetKey, float range) : base(tree){
		this.range = range;
		this.targetKey = targetKey;
	}

	public override void Poke (Parent parent)
	{
		base.Poke (parent);

		GameObject target = tree.GetFromBlackboard<GameObject>(targetKey);
		if(target != null && tree.actions.IsInRange(target, range)){
			parent.ReceiveStatus(Status.Success);
		}
		else{
			parent.ReceiveStatus(Status.Failure);
		}
	}
}
