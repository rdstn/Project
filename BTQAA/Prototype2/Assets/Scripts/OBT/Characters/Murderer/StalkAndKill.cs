using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StalkAndKill : BehaviourTree {

	string targetKey;
	GameObject target;
	
	//public StalkAndKill(EventManager eventManager, 
	//                 string targetKey) : 
	//base (eventManager){
	//	this.targetKey = targetKey;
	//}
	
	public StalkAndKill(EventManager eventManager, 
	                 string targetKey, GameObject target) : 
	base (eventManager){
		this.targetKey = targetKey;
		this.target = target;
		AddToBlackboard(targetKey, target);
	}
	
	//public StalkAndKill(EventManager eventManager, Dictionary<string, object> blackboard, 
	//                 string targetKey) : 
	//base(eventManager, blackboard){
	//	this.targetKey = targetKey;
	//}
	
	public StalkAndKill(EventManager eventManager, Dictionary<string, object> blackboard, 
	                 string targetKey, GameObject target) : 
	base(eventManager, blackboard){
		this.targetKey = targetKey;
		this.target = target;
		AddToBlackboard(targetKey, target);
	}

	//TODO RETHINK LOOP
	public override void constructTree ()
	{
		if(target != null){
			base.root = new Root(this, new Node[] {
				new Repeater(this, 0, Repeater.RepeatType.UntilSuccess, new Node[] {
					new Sequence(new Node[] {
						new LinkTree<Stalk>(this, new Node[0], new object[] {this.eventManager, targetKey, target}),
						new LinkTree<StalkWait>(this, new Node[0], new object[] {this.eventManager, targetKey, target}),
						new LinkTree<Murder>(this, new Node[0], new object[] {this.eventManager, targetKey, target})
					})
				})
			});
		}
		else{
			base.root = new Root(this, new Node[] {
				new Repeater(this, 0, Repeater.RepeatType.UntilSuccess, new Node[] {
					new Sequence(new Node[] {
						new LinkTree<Stalk>(this, new Node[0], new object[] {this.eventManager, targetKey}),
						new LinkTree<StalkWait>(this, new Node[0], new object[] {this.eventManager, targetKey}),
						new LinkTree<Murder>(this, new Node[0], new object[] {this.eventManager, targetKey}),
					})	
				})
			});
		}
	}
}
