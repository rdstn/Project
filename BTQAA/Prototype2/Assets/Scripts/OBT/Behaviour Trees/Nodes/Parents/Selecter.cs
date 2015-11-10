using UnityEngine;
using System.Collections;

public class Selecter : Parent {

	private int nextChild = 0;

	public Selecter(Node[] children) : base(children){

	}

	public override void Poke(Parent parent){
		base.Poke(parent);
		nextChild = 0;
		base.children[nextChild++].Poke(this);
	}

	public override void ReceiveStatus (Status status)
	{
		if(status == Status.Failure){
			if(nextChild >= base.children.Count){
				base.parent.ReceiveStatus(Status.Failure);
			}
			else{
				base.children[nextChild++].Poke(this);
			}
		}
		else{
			base.parent.ReceiveStatus(Status.Success);
		}
	}
}
