using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Speak : BehaviourTree {

	string targetKey;
	string sentenceKey;

	float seconds;

	int priority;

	public Speak(EventManager eventManager, 
	                 string targetKey, string sentenceKey, float seconds, int priority) : 
	base (eventManager){
		this.targetKey = targetKey;
		this.sentenceKey = sentenceKey;
		this.seconds = seconds;
		this.priority = priority;
	}



	public Speak(EventManager eventManager, 
	             string targetKey, string sentenceKey, Sentence sentence, float seconds, int priority) : 
	base (eventManager){
		this.targetKey = targetKey;
		this.sentenceKey = sentenceKey;
		AddToBlackboard(sentenceKey, sentence);
		this.seconds = seconds;
		this.priority = priority;
	}
	
	public Speak(EventManager eventManager, 
	             string targetKey, GameObject target, string sentenceKey, float seconds, int priority) : 
	base (eventManager){
		this.targetKey = targetKey;
		this.sentenceKey = sentenceKey;
		AddToBlackboard(targetKey, target);
		this.seconds = seconds;
		this.priority = priority;
	}

	public Speak(EventManager eventManager, 
	             string targetKey, GameObject target, string sentenceKey, Sentence sentence, float seconds, int priority) : 
	base (eventManager){
		this.targetKey = targetKey;
		this.sentenceKey = sentenceKey;
		AddToBlackboard(targetKey, target);
		AddToBlackboard(sentenceKey, sentence);
		this.seconds = seconds;
		this.priority = priority;
	}
	
	public Speak(EventManager eventManager, Dictionary<string, object> blackboard, 
	             string targetKey, string sentenceKey, float seconds, int priority) : 
	base(eventManager, blackboard){
		this.targetKey = targetKey;
		this.sentenceKey = sentenceKey;
		this.seconds = seconds;
		this.priority = priority;
	}

	public Speak(EventManager eventManager, Dictionary<string, object> blackboard, 
	             string targetKey, string sentenceKey, Sentence sentence, float seconds, int priority) : 
	base(eventManager, blackboard){
		this.targetKey = targetKey;
		this.sentenceKey = sentenceKey;
		AddToBlackboard(sentenceKey, sentence);
		this.seconds = seconds;
		this.priority = priority;
	}
	
	public Speak(EventManager eventManager, Dictionary<string, object> blackboard, 
	             string targetKey, GameObject target, string sentenceKey, float seconds, int priority) : 
	base(eventManager, blackboard){
		this.targetKey = targetKey;
		this.sentenceKey = sentenceKey;
		AddToBlackboard(targetKey, target);
		this.seconds = seconds;
		this.priority = priority;
	}

	public Speak(EventManager eventManager, Dictionary<string, object> blackboard, 
	             string targetKey, GameObject target, string sentenceKey, Sentence sentence, float seconds, int priority) : 
	base(eventManager, blackboard){
		this.targetKey = targetKey;
		this.sentenceKey = sentenceKey;
		AddToBlackboard(targetKey, target);
		AddToBlackboard(sentenceKey, sentence);
		this.seconds = seconds;
		this.priority = priority;
	}

	//TODO Need to reconstruct the tree every time, also make sure
	//that changes to the args don't happen in this tree or this subtree
	//Potential solution of having a new tree for add behaviour tree that
	//reconstrcuts every time
	public override void constructTree ()
	{
		GameObject responseTarget = GetFromBlackboard<GameObject>("self");
		Sentence responseSentence = GetFromBlackboard<Sentence>(sentenceKey);
		float responseSeconds = seconds;
		object[] args = {responseTarget, responseSentence, responseSeconds};
		object[] args2 = {this.eventManager, this.blackboard, targetKey, seconds, priority-1}; //Synchronised wait

		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new LinkTree<SynchronisedWait>(this, new Node[0], args2),
				new DisplaySentence(this, sentenceKey, seconds),
				new Wait(this, seconds),
				new Repeater(this, 0, Repeater.RepeatType.UntilSuccess, new Node[] {
					new AddBehaviourTree<Respond>(this, targetKey, priority, false, args),
				}),
				new WhisperSentence(this, sentenceKey, targetKey),
			})
		});


	}
}
