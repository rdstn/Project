using UnityEngine;
using System.Collections;

public class FindRoom : BehaviourTree {
	string receptionistKey;
	string questionKey = "question";
	string receptionKey = "reception";

	public FindRoom(EventManager eventManager, string receptionistKey, GameObject receptionist, GameObject reception) : base(eventManager){
		this.receptionistKey = receptionistKey;
		AddToBlackboard(receptionistKey, receptionist);
		AddToBlackboard("question", new Sentence(eventManager.gameObject, Sentence.Verb.StayingIn, null));

		AddToBlackboard(receptionKey, reception);
	}

	public override void constructTree ()
	{
		object[] args = {this.eventManager, this.blackboard, receptionistKey, questionKey, 5, 8};

		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				//new Follow(this, receptionistKey),
				new SetRandomRoom(this, "destination", receptionKey),
				new TravelTo(this, "destination"),
				new LinkTree<Speak>(this, new Node[0], args),
			})
		});
	}
}
