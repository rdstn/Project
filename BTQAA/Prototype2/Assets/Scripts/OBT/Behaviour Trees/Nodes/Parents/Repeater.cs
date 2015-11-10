using UnityEngine;
using System.Collections;

public class Repeater : Parent {

	int repeatNTimes = 0;
	int repeated = 0;

	public enum RepeatType {
		UntilSuccess,
		UntilFail,
		Forever,
	};

	RepeatType repeatType = RepeatType.Forever;

	public Repeater(BehaviourTree tree, int repeatNTimes, RepeatType repeatType, Node[] children) : base(children){
		this.repeatNTimes = repeatNTimes;
		this.repeatType = repeatType;
		base.tree = tree;
	}

	public override void Poke (Parent parent)
	{
		repeated = 0;
		base.Poke (parent);
		base.children[0].Poke(this);
	}

	public void Repeat(){
		base.children[0].Poke (this);
	}

	public override void ReceiveStatus (Status status)
	{
		if(repeatType == RepeatType.UntilSuccess){
			if(status == Status.Success){
				parent.ReceiveStatus(Status.Success);
				return;
			}
		}
		if(repeatType == RepeatType.UntilFail){
			if(status == Status.Failure){
				parent.ReceiveStatus(Status.Failure);
				return;
			}
		}
		if(repeatNTimes > 0){
			repeated++;
			if(repeated >= repeatNTimes){
				return;
			}
		}
		tree.actions.Repeat(this);
	}

	public override void Interrupt ()
	{
		tree.actions.RepeatInterrupt();
		base.Interrupt ();
	}
}
