using UnityEngine;
using System.Collections;

public class SetAloneWith : Leaf_Instant {

	private string locationKey;
	private string aloneWithKey;

	public SetAloneWith(BehaviourTree tree, string locationKey, string aloneWithKey) : base(tree){
		this.locationKey = locationKey;
		this.aloneWithKey = aloneWithKey;
	}

	public override void Poke (Parent parent)
	{
		base.Poke (parent);
		GameObject location = tree.GetFromBlackboard<GameObject>(locationKey);
		if(location == null){
			parent.ReceiveStatus(Status.Failure);
		}
		else{
			GameObject aloneWith = tree.actions.GetAloneWith(location);
			if(aloneWith != null){
				tree.AddToBlackboard(aloneWithKey, aloneWith);
				parent.ReceiveStatus(Status.Success);
			}
			else{
				parent.ReceiveStatus(Status.Failure);
			}
		}
	}
}
