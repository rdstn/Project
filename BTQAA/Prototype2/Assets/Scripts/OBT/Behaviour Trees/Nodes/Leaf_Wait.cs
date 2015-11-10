using UnityEngine;
using System.Collections;

public abstract class Leaf_Wait : Node{

	public Leaf_Wait(BehaviourTree tree){
		base.tree = tree;
	}

	public abstract void ReceiveStatus(Node.Status status);
}
