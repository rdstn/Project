using UnityEngine;
using System.Collections;

public abstract class Leaf_Instant : Node {

	public Leaf_Instant(BehaviourTree tree){
		base.tree = tree;
	}

	public override void Interrupt ()
	{
		//Do nothing
	}
}
