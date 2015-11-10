using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stalk : BehaviourTree {
	
	string targetKey;

	public Stalk(EventManager eventManager, 
	             string targetKey) : 
	base (eventManager){
		this.targetKey = targetKey;
	}
	
	public Stalk(EventManager eventManager, 
	             string targetKey, GameObject target) : 
	base (eventManager){
		this.targetKey = targetKey;
		AddToBlackboard(targetKey, target);
	}
	
	public Stalk(EventManager eventManager, Dictionary<string, object> blackboard, 
	             string targetKey) : 
	base(eventManager, blackboard){
		this.targetKey = targetKey;
	}

	public Stalk(EventManager eventManager, Dictionary<string, object> blackboard, 
	             string targetKey, GameObject target) : 
	base(eventManager, blackboard){
		this.targetKey = targetKey;
		AddToBlackboard(targetKey, target);
	}
	
	public override void constructTree ()
	{
		base.root = new Root(this, new Node[] {
			new Repeater(this, 0, Repeater.RepeatType.UntilSuccess, new Node[] {
				new Selecter(new Node[] {
					//Sequence returns true if characters are in the room together :)
					new Sequence(new Node[] {
						new SetLocation(this, "selfLocation", "self"),
						new SetLocation(this, "targetLocation", targetKey),
						new CheckBlackboard(this, "selfLocation", "targetLocation")
					}),
					//Otherwise always returns false, so keeps repeating
					new EnforceResult(false, new Node[] {
						new FirstToFinish(new Node[] {
							new Wait(this, 1f),
							new Sequence(new Node[] {
								//Returns true if in range or has managed to get to thingy
								//new DebugMessage(this, "TESTING RANGE"),
								new Selecter(new Node[] {
									new CheckInRange(this, targetKey, 0.3f),
									new Sequence(new Node[] {
										//new DebugMessage(this, "OUT OF RANGE, MOVING TO TARGET"),
										//new DebugMessage(this, "Attempting to Set Point!"),
										new SetPoint(this, "targetPoint", targetKey),
										//new DebugMessage(this, "Moving to Target!"),
										new TravelTo(this, "targetPoint")
									})
								}),
								//If not in same room, go to random point in room
								new Selecter(new Node[] {
									new Inverter(new Node[] {
										new Sequence(new Node[] {
											new SetLocation(this, "selfLocation", "self"),
											new SetLocation(this, "targetLocation", targetKey),
											new CheckBlackboard(this, "selfLocation", "targetLocation")
										}),
									}),
									new Sequence(new Node[] {
										new SetLocation(this, "targetLocation", targetKey),
										new SetRandomRoom(this, "targetPoint", "targetLocation"),
										new TravelTo(this, "targetPoint")
									}),
								}),
							})
						}),
					})
				})
			})
		});
	}
}
