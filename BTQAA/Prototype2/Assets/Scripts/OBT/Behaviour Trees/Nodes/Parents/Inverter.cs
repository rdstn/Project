using UnityEngine;
using System.Collections;

public class Inverter : Parent {

	public Inverter(Node[] children) : base(children){

	}

	public override void Poke (Parent parent)
	{
		base.Poke (parent);
		children[0].Poke(this);
	}

	public override void ReceiveStatus (Status status)
	{
		if(status == Status.Success){
			parent.ReceiveStatus(Status.Failure);
		}
		else{
			parent.ReceiveStatus(Status.Success);
		}
	}
}
