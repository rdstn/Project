using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SecretRomance : BehaviourTree {

	string targetKey;

	float seconds = 4;
	float range = 5;
	

	public SecretRomance(EventManager eventManager, string targetKey) : base(eventManager){
		this.targetKey = targetKey;

		loveSentences();
	}

	public SecretRomance(EventManager eventManager, string targetKey, GameObject target) : base(eventManager){
		this.targetKey = targetKey;
		AddToBlackboard(targetKey, target);

		loveSentences();
	}

	public SecretRomance(EventManager eventManager, Dictionary<string, object> blackboard,
	                     string targetKey) : base(eventManager, blackboard){
		this.targetKey = targetKey;

		loveSentences();
	}
	
	public SecretRomance(EventManager eventManager, Dictionary<string, object> blackboard,
	                     string targetKey, GameObject target) : base(eventManager, blackboard){
		this.targetKey = targetKey;
		AddToBlackboard(targetKey, target);

		loveSentences();
	}

	private void loveSentences(){
		AddToBlackboard("loveSentence", new Sentence(null, Sentence.Verb.Love, null));
	}

	public override void constructTree ()
	{

		object[] args = {this.eventManager, this.blackboard, targetKey, 10, 9}; // Wait
		object[] args2 = {this.eventManager, this.blackboard, targetKey, "destination", 10}; //TellTravelTo
		object[] args3 = {this.eventManager, this.blackboard, targetKey, 11, seconds}; //Love

		GameObject noun1 = GetFromBlackboard<GameObject>("self");
		GameObject noun2 = GetFromBlackboard<GameObject>(targetKey);
		AddToBlackboard("loveInfo", new Sentence(noun1, Sentence.Verb.Love, noun2));

		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new Follow(this, targetKey),
				new LinkTree<SynchronisedWait>(this, new Node[0], args),
				new Repeater(this, 3, Repeater.RepeatType.UntilSuccess, new Node[] {
					new Selecter(new Node[] {
						//Alone and Happy
						new Sequence(new Node[] {
							new SetLocation(this, "location", "self"),
							new SetAloneWith(this, "location", "aloneWith"),
							new CheckBlackboard(this, "aloneWith", targetKey),
							new Follow(this, targetKey),
							new DisplaySentence(this, "loveSentence", seconds),
							new LinkTree<TellLove>(this, new Node[0], args3),
							new BroadcastSentence(this, "loveInfo", range),
						}),
						//Find a new place TODO DOES THIS ACTUALLY WORK?
						new EnforceResult(false, new Node[] {
							new Sequence(new Node[] {
								new Follow(this, targetKey),
								new SetEmptyRoom(this, "emptyRoom"),
								new SetRandomRoom(this, "destination", "emptyRoom"),
								new ConstructSentence(this, targetKey, Sentence.Verb.Arrow, "emptyRoom", "tellSentence"),
								new DisplaySentence(this, "tellSentence", seconds), 
								new LinkTree<TellTravelTo>(this, new Node[0], args2), //Need to guarantee this is finished
								new Wait(this, 5f),
								new Repeater(this, 0, Repeater.RepeatType.UntilSuccess, new Node[] {
									new Sequence(new Node[] {
										new Follow(this, targetKey, 0.3f),
										new SetLocation(this, "location", "self"),
									})
								})
							})
						})
					})
				})
			})
		});
	}
}
