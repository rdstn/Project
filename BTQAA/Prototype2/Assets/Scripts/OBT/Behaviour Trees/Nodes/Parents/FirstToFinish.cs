using UnityEngine;
using System.Collections;

public class FirstToFinish : Parent {

	bool active = false;

	public FirstToFinish(Node[] children) : base(children){
	}

	public override void Poke (Parent parent)
	{
		active = true;
		base.Poke (parent);
		for(int i = 0; i < base.children.Count; i++){
			if(!active){
				break;
			}
			base.children[i].Poke(this);
		}
	}

	public override void ReceiveStatus (Status status)
	{
		active = false;
		//Debug.Log (status);
		for(int i = 0; i < base.children.Count; i++){
			base.children[i].Interrupt();
		}
		parent.ReceiveStatus(status);
	}
}
