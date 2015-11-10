using UnityEngine;
using System.Collections;

public class EnforceResult : Parent {

	private bool result;

	public EnforceResult(bool result, Node[] children) : base(children){
		this.result = result;
	}

	public override void Poke (Parent parent)
	{
		base.Poke (parent);
		children[0].Poke(this);
	}

	public override void ReceiveStatus (Status status)
	{
		if(result){
			parent.ReceiveStatus(Status.Success);
		}
		else{
			parent.ReceiveStatus(Status.Failure);
		}
	}
}
