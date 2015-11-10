using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TravelToTree : BehaviourTree {

	public TravelToTree(EventManager eventManager, Vector3 destination) : base(eventManager){
		AddToBlackboard("destination", destination);
	}

	public TravelToTree(EventManager eventManager, Dictionary<string, object> blackboard, Vector3 destination) : base(eventManager, blackboard){
		AddToBlackboard("destination", destination);
	}

	public override void constructTree ()
	{
		base.root = new Root(this, new Node[] {
			new TravelTo(this, "destination"),
		});
	}
}
