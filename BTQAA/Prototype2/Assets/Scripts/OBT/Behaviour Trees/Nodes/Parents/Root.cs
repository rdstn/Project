using UnityEngine;
using System.Collections;

public class Root : Parent {

	public Root(BehaviourTree tree, Node[] children) : base(children){
		base.tree = tree;
	}

	public void Poke(){
		base.children[0].Poke(this);
	}

	public override void Poke (Parent parent)
	{
		base.Poke (parent);
		base.children[0].Poke(this);
	}

	public override void ReceiveStatus(Node.Status status){
		if(parent == null){
			if(status == Node.Status.Success){
				base.tree.Finished(true);
			}
			else{
				base.tree.Finished(false);
			}
		}
		else{
			parent.ReceiveStatus(status);
		}
	}

}
