using UnityEngine;
using System.Collections;

public class RandomFail : Leaf_Instant {

	float failChance;

	public RandomFail(BehaviourTree tree, float failChance) : base(tree){
		this.failChance = failChance;
	}

	public override void Poke (Parent parent)
	{
		base.Poke (parent);
		if (Random.value < failChance){
			parent.ReceiveStatus(Status.Failure);
		}
		else{
			parent.ReceiveStatus(Status.Success);
		}
	}
}
