using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CallPolice : BehaviourTree {

	private string detectiveKey;
	private string sentenceKey;

	public CallPolice(EventManager eventManager, GameObject detective, Sentence sentence) : base(eventManager){
		detectiveKey = "detective";
		sentenceKey = "sentenceKey";
		AddToBlackboard(detectiveKey, detective);
		AddToBlackboard(sentenceKey, sentence);
		AddToBlackboard("police", new Sentence(null, Sentence.Verb.police, null));
	}
	
	public CallPolice(EventManager eventManager, Dictionary<string, object> blackboard) : base(eventManager, blackboard){

	}

	public override void constructTree ()
	{
		
		object[] args = {this.eventManager, this.blackboard, detectiveKey, sentenceKey, 5, 18};//Speak
		
		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new DisplaySentence(this, "police", 4),
				new Wait(this, 4),
				new LinkTree<Speak>(this, new Node[0], args),
			})
		});
	}

}
