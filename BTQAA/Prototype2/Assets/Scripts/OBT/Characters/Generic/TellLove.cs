using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TellLove : BehaviourTree {

	int priority;
	string targetKey;
	float seconds;

	public TellLove(EventManager eventManager, string targetKey, int priority, float seconds) : base(eventManager){
		this.priority = priority;
		this.targetKey = targetKey;
		this.seconds = seconds;
	}

	public TellLove(EventManager eventManager, Dictionary<string, object> blackboard, string targetKey, int priority, float seconds) : base(eventManager, blackboard){
		this.priority = priority;
		this.targetKey = targetKey;
		this.seconds = seconds;
	}

	public override void constructTree ()
	{
		object[] args = {seconds};

		base.root = new Root(this, new Node[] {
			new AddBehaviourTree<Love>(this, targetKey, priority, false, args),
		});
	}
}
