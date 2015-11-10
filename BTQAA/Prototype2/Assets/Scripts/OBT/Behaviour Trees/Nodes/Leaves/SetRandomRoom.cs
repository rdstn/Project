using UnityEngine;
using System.Collections;

public class SetRandomRoom : Leaf_Instant {

	private string destinationKey;
	private string roomKey;

	public SetRandomRoom(BehaviourTree tree, string destinationKey) : base(tree){
		this.destinationKey = destinationKey;
	}

	public SetRandomRoom(BehaviourTree tree, string destinationKey, string roomKey) : base(tree){
		this.destinationKey = destinationKey;
		this.roomKey = roomKey;
	}
	
	public override void Poke (Parent parent)
	{
		base.Poke (parent);
		if(roomKey == null){
			tree.AddToBlackboard(destinationKey, tree.actions.RandomPointInRandomRoom());
		}
		else{
			GameObject room = tree.GetFromBlackboard<GameObject>(roomKey);
			if(room == null){
				parent.ReceiveStatus(Status.Failure);
				return;
			}
			tree.AddToBlackboard(destinationKey, tree.actions.RandomPointInRoom(room));
		}
		parent.ReceiveStatus(Status.Success);
	}
}
