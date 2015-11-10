using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bar : BehaviourTree {
	
	float minTime;
	float maxTime;
	
	public Bar(EventManager eventManager, float minTime, float maxTime, GameObject room) : base(eventManager){
		this.minTime = minTime;
		this.maxTime = maxTime;
		
		AddToBlackboard("room", room);
		AddToBlackboard("beer", new Sentence(null, Sentence.Verb.Beer, null));
	}
	
	public override void constructTree ()
	{
		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new SetRandomRoom(this, "destination", "room"),
				new TravelTo(this, "destination"),
				new DisplaySentence(this, "beer", 3),
				new Wait(this, minTime, maxTime),
			})
		});
	}
}
