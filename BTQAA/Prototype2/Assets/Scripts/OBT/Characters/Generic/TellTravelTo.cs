using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TellTravelTo : BehaviourTree {

	string targetKey;
	string destinationKey;
	int priority;

	public TellTravelTo(EventManager eventManager, string targetKey, string destinationKey, int priority) : base(eventManager){
		this.targetKey = targetKey;
		this.priority = priority;
		this.destinationKey = destinationKey;
	}
	
	public TellTravelTo(EventManager eventManager, Dictionary<string, object> blackboard, string targetKey, string destinationKey, int priority) : base(eventManager, blackboard){
		this.targetKey = targetKey;
		this.priority = priority;
		this.destinationKey = destinationKey;
	}

	public override void constructTree ()
	{
		Vector3 destination = GetFromBlackboard<Vector3>(destinationKey);
		object[] args = {destination};

		base.root = new Root(this, new Node[] {
			new AddBehaviourTree<TravelToTree>(this, targetKey, priority, false, args),
		});
	}
}
