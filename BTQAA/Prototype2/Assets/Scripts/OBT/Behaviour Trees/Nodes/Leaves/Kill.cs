using UnityEngine;
using System.Collections;

public class Kill : Leaf_Instant {

	private string victimKey;

	public Kill(BehaviourTree tree, string victimKey) : base(tree){
		this.victimKey = victimKey;
	}

	public override void Poke (Parent parent)
	{
		base.Poke(parent);
		GameObject victim = tree.GetFromBlackboard<GameObject>(victimKey);
		if(victim == default(GameObject)){
			parent.ReceiveStatus(Status.Failure);
		}
		else{
			tree.actions.Kill(victim, this);
		}
	}

	public void ReceiveStatus (Status status)
	{
		base.parent.ReceiveStatus(status);
	}
}
