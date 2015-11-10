using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TV : BehaviourTree {
	
	float minTime;
	float maxTime;
	
	public TV(EventManager eventManager, float minTime, float maxTime, GameObject room) : base(eventManager){
		this.minTime = minTime;
		this.maxTime = maxTime;
		
		AddToBlackboard("room", room);
		AddToBlackboard("tv", new Sentence(null, Sentence.Verb.TV, null));
	}
	
	public override void constructTree ()
	{
		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new SetRandomRoom(this, "destination", "room"),
				new TravelTo(this, "destination"),
				new DisplaySentence(this, "tv", 3),
				new Wait(this, minTime, maxTime),
			})
		});
	}
}
