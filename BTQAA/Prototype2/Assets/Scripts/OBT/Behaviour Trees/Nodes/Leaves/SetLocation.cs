using UnityEngine;
using System.Collections;

public class SetLocation : Leaf_Instant {
	
	private string locationKey;
	private string targetKey;
	
	public SetLocation(BehaviourTree tree, string locationKey, string targetKey) : base(tree){
		this.locationKey = locationKey;
		this.targetKey = targetKey;
	}
	
	public override void Poke (Parent parent)
	{
		base.Poke (parent);
		GameObject target = tree.GetFromBlackboard<GameObject>(targetKey);
		if(target == null){
			//Debug.Log ("Target Null");
			parent.ReceiveStatus(Status.Failure);
			return;
		}
		
		GameObject location = tree.actions.FindRoomTargetIsIn(target);
		if(location == null){
			//Debug.Log ("Location Null");
			parent.ReceiveStatus(Status.Failure);
			return;
		}
		
		tree.AddToBlackboard(locationKey, location);
		parent.ReceiveStatus(Status.Success);
	}
}
