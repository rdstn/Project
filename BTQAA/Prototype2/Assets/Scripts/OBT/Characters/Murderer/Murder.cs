using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Murder : BehaviourTree {
	string targetKey;
	
	public Murder(EventManager eventManager, 
	             string targetKey) : 
	base (eventManager){
		this.targetKey = targetKey;
	}
	
	public Murder(EventManager eventManager, 
	             string targetKey, GameObject target) : 
	base (eventManager){
		this.targetKey = targetKey;
		AddToBlackboard(targetKey, target);
	}
	
	public Murder(EventManager eventManager, Dictionary<string, object> blackboard, 
	             string targetKey) : 
	base(eventManager, blackboard){
		this.targetKey = targetKey;
	}
	
	public Murder(EventManager eventManager, Dictionary<string, object> blackboard, 
	             string targetKey, GameObject target) : 
	base(eventManager, blackboard){
		this.targetKey = targetKey;
		AddToBlackboard(targetKey, target);
	}

	public override void constructTree ()
	{
		GameObject target = GetFromBlackboard<GameObject>(targetKey);
		AddToBlackboard("murderSentence", new Sentence(eventManager.gameObject, Sentence.Verb.Murder, target));

		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new Follow(this, targetKey),
				new BroadcastSentence(this, "murderSentence" ,4),
				new Kill(this, targetKey),
			})
		});
	}
}
