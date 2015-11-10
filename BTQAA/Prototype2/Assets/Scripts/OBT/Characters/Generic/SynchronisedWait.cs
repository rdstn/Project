using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SynchronisedWait : BehaviourTree {

	float time;
	string targetKey;
	int priority;

	public SynchronisedWait(EventManager eventManager, string targetKey, float time, int priority) : base(eventManager){
		this.time = time;
		this.targetKey = targetKey;
		this.priority = priority;
	}

	public SynchronisedWait(EventManager eventManager,Dictionary<string, object> blackboard, string targetKey, float time, int priority) : base(eventManager, blackboard){
		this.time = time;
		this.targetKey = targetKey;
		this.priority = priority;
	}

	public override void constructTree ()
	{
		object[] args = {time};

		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new AddBehaviourTree<WaitTree>(this, targetKey, priority, false, args),
				new AddBehaviourTree<WaitTree>(this, "self", priority, true, args)
			})
		});
	}
}
