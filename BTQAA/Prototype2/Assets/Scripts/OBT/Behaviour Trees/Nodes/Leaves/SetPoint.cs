using UnityEngine;
using System.Collections;

public class SetPoint : Leaf_Instant {
	
	private string targetKey;
	private string destinationKey;
	
	public SetPoint(BehaviourTree tree, string destinationKey, string targetKey) : base(tree){
		this.targetKey = targetKey;
		this.destinationKey = destinationKey;
	}
	
	public override void Poke (Parent parent)
	{
		base.Poke (parent);
		GameObject target = tree.GetFromBlackboard<GameObject>(targetKey);
		if(target != null){
			///Debug.Log("targetPoint");
			//Debug.Log (target);
			//Debug.Log (target.transform.position);
			tree.AddToBlackboard(destinationKey, target.transform.position);
			parent.ReceiveStatus(Status.Success);
		}
		else{
			parent.ReceiveStatus(Status.Failure);
		}
	}
}
