using UnityEngine;
using System.Collections;

public class ReceptionistIdle : BehaviourTree {

	float min;
	float max;

	public ReceptionistIdle(EventManager eventManager, float min, float max, GameObject reception) : base(eventManager){
		this.min = min;
		this.max = max;

		AddToBlackboard("room", reception);
	}

	public override void constructTree ()
	{
		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new Wait(this, min, max),
				new SetRandomRoom(this, "destination", "room"),
				new TravelTo(this, "destination"),
			})
		});
	}
}
