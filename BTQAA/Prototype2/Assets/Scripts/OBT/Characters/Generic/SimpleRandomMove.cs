using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleRandomMove : BehaviourTree {
	public SimpleRandomMove(EventManager eventManager) :
		base (eventManager){
	}

	public SimpleRandomMove(EventManager eventManager, Dictionary<string, object> blackboard) :
	base (eventManager, blackboard){
	}

	public override void constructTree ()
	{
		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new SetRandomRoom(this, "destination"),
				new TravelTo(this, "destination")})});
	}
}
