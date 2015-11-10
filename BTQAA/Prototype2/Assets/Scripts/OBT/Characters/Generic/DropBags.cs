using UnityEngine;
using System.Collections;

public class DropBags : BehaviourTree {

	string roomKey;

	public DropBags(EventManager eventManager, string roomKey, GameObject room) : base(eventManager){
		this.roomKey = roomKey;
		AddToBlackboard(roomKey, room);
		AddToBlackboard("bags", new Sentence(null, Sentence.Verb.Bag, null));
	}

	public override void constructTree ()
	{
		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new SetRandomRoom(this, "destination", roomKey),
				new TravelTo(this, "destination"),
				new DisplaySentence(this, "bags", 3),
				new Wait(this, 3),
			}),
		});
	}
}
