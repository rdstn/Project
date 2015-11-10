using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Respond : BehaviourTree {

	float seconds;

	Sentence sentence;
	GameObject target;

	public Respond(EventManager eventManager, 
	                 GameObject target, Sentence sentence, float seconds) : 
	base (eventManager){
		this.seconds = seconds;
		this.sentence = sentence;
		this.target = target;
	}

	public Respond(EventManager eventManager, Dictionary<string, object> blackboard,
	               GameObject target, Sentence sentence, float seconds) : 
	base (eventManager, blackboard){
		this.seconds = seconds;
		this.sentence = sentence;
		this.target = target;
	}

	public override void constructTree ()
	{
		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new SetResponse(this, sentence, "response"),
				new DisplaySentence(this, "response", seconds),
				new WhisperSentence(this, "response", target),
			})
		});
	}
}
