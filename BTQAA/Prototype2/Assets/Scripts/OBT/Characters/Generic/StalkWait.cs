using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StalkWait : BehaviourTree {

	string targetKey;
	
	public StalkWait(EventManager eventManager, 
	             string targetKey) : 
	base (eventManager){
		this.targetKey = targetKey;
	}
	
	public StalkWait(EventManager eventManager, 
	             string targetKey, GameObject target) : 
	base (eventManager){
		this.targetKey = targetKey;
		AddToBlackboard(targetKey, target);
	}
	
	public StalkWait(EventManager eventManager, Dictionary<string, object> blackboard, 
	             string targetKey) : 
	base(eventManager, blackboard){
		this.targetKey = targetKey;
	}
	
	public StalkWait(EventManager eventManager, Dictionary<string, object> blackboard, 
	             string targetKey, GameObject target) : 
	base(eventManager, blackboard){
		this.targetKey = targetKey;
		AddToBlackboard(targetKey, target);
	}

	public override void constructTree ()
	{
		//Returns True if target is alone in room with enemy, false if they are not in the same room, otherwise does nothing
		base.root = new Root(this, new Node[] {
			//new Repeater(this, 0, Repeater.RepeatType.Forever, new Node[] {
				new FirstToFinish(new Node[] {
					new Repeater(this, 0, Repeater.RepeatType.UntilSuccess, new Node [] {
						new Sequence(new Node[] {
							new SetLocation(this, "selfLocation", "self"),
							new SetAloneWith(this, "selfLocation", "aloneWith"),
							new CheckBlackboard(this, "aloneWith", targetKey)
						}),
					}),
					new Repeater(this, 0, Repeater.RepeatType.UntilFail, new Node [] {
						new Sequence(new Node[] {
							new SetLocation(this, "selfLocation", "self"),
							new SetLocation(this, "targetLocation", targetKey),
							new CheckBlackboard(this, "selfLocation", "targetLocation")
						}),
					})
				})
			//})
		});
	}
}
