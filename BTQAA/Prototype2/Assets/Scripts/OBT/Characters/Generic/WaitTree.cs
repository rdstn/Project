using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaitTree : BehaviourTree {
	float time;
	float minTime;
	float maxTime;

	bool randomTime = false;

	public WaitTree(EventManager eventManager, 
	                float time) : 
	base (eventManager){
		this.time = time;
	}

	public WaitTree(EventManager eventManager, 
	                float minTime, float maxTime) : 
	base (eventManager){
		this.minTime = minTime;
		this.maxTime = maxTime;

		randomTime = true;
	}
	
	public WaitTree(EventManager eventManager, Dictionary<string, object> blackboard, 
	                float time) : 
	base(eventManager, blackboard){
		this.time = time;
	}

	public WaitTree(EventManager eventManager, Dictionary<string, object> blackboard, 
	                float minTime, float maxTime) : 
	base(eventManager, blackboard){
		this.minTime = minTime;
		this.maxTime = maxTime;

		randomTime= true;
	}

	public override void constructTree ()
	{
		if(!randomTime){
			base.root = new Root(this, new Node[] {
				new Wait(this, time)
			});
		}
		else{
			base.root = new Root(this, new Node[] {
				new Wait(this, minTime, maxTime)
			});
		}
	}
}
