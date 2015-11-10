using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Toilet : BehaviourTree {
	
	float minTime;
	float maxTime;
	
	public Toilet(EventManager eventManager, float minTime, float maxTime, GameObject room) : base(eventManager){
		this.minTime = minTime;
		this.maxTime = maxTime;
		
		AddToBlackboard("room", room);
		AddToBlackboard("toilet", new Sentence(null, Sentence.Verb.Toilet, null));
	}
	
	public override void constructTree ()
	{
		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new SetRandomRoom(this, "destination", "room"),
				new TravelTo(this, "destination"),
				new DisplaySentence(this, "toilet", 3),
				new Wait(this, minTime, maxTime),
			})
		});
	}
}
