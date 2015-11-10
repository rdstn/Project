using UnityEngine;
using System.Collections;

public class CopyToNewKey : Leaf_Instant {

	string key;
	string copyKey;

	public CopyToNewKey(BehaviourTree tree, string key, string copyKey) : base (tree){
		this.key = key;
		this.copyKey = copyKey;
	}

	public override void Poke (Parent parent)
	{
		base.Poke (parent);

		object obj = tree.GetFromBlackboard<object>(key);
		if(obj != null){
			tree.AddToBlackboard(copyKey, obj);
			parent.ReceiveStatus(Status.Success);
		}
		else{
			parent.ReceiveStatus(Status.Failure);
		}
	}
}
