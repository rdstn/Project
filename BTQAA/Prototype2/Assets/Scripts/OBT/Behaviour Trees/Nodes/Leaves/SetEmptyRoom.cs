using UnityEngine;
using System.Collections;

public class SetEmptyRoom : Leaf_Instant {

	private string emptyRoomKey;

	public SetEmptyRoom(BehaviourTree tree, string emptyRoomKey) : base(tree){
		this.emptyRoomKey = emptyRoomKey;
	}

	public override void Poke (Parent parent)
	{
		base.Poke (parent);
		GameObject room = tree.actions.GetEmptyRoom();

		if(room != null){
			tree.AddToBlackboard(emptyRoomKey, room);
			parent.ReceiveStatus(Status.Success);
		}
		else{
			parent.ReceiveStatus(Status.Failure);
		}

	}
}
