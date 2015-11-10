using UnityEngine;
using System.Collections;

public class Love : BehaviourTree {

	float seconds;

	public Love(EventManager eventManager, float seconds) : base(eventManager){
		this.seconds = seconds;
		AddToBlackboard("loveSentence", new Sentence(null, Sentence.Verb.Love, null));
	}

	public override void constructTree ()
	{
		base.root = new Root(this, new Node[] {
			new DisplaySentence(this, "loveSentence", seconds)
		});
	}
}
