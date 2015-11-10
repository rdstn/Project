using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Snooker : BehaviourTree {

	GameObject room;
	GameObject opponent;

	public Snooker(EventManager eventManager, GameObject room, GameObject opponent) : base(eventManager){

		AddToBlackboard("room", room);
		AddToBlackboard("opponent", opponent);

		this.room = room;
		this.opponent = opponent;
	}
	
	public override void constructTree ()
	{
		object[] args = {this.eventManager, this.blackboard, "opponent", 10, 3}; // sync wait
		object[] args2 = {this.eventManager, this.blackboard, "opponent", "destination", 4}; //telltravelto
		object[] args3 = {this.eventManager, room, opponent, 5}; //snooker

		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new Follow(this, "opponent"),
				new LinkTree<SynchronisedWait>(this, new Node[0], args),
				new SetRandomRoom(this, "destination", "room"),
				//new TravelTo(this, "destination"),
				//new Wait(this, minTime, maxTime),
				new ConstructSentence(this, "opponent", Sentence.Verb.Arrow, "room", "tellSentence"),
				new DisplaySentence(this, "tellSentence", 2),
				new LinkTree<TellTravelTo>(this, new Node[0], args2),
				new LinkTree<SnookerShot>(this, new Node[0], args3),
			})
		});
	}
}
