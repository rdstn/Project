using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Investigate : BehaviourTree {

	private string victimKey;
	private string receptionistKey;

	public Investigate(EventManager eventManager, GameObject victim, GameObject receptionist, GameObject jail) : base(eventManager){
		this.victimKey = "victim";
		AddToBlackboard(victimKey, victim);

		this.receptionistKey = "receptionist";
		AddToBlackboard(receptionistKey, receptionist);

		//CHEAT
		AddToBlackboard("murderer", victim.GetComponent<EventManager>().GetKiller());

		AddToBlackboard("hello", new Sentence(null, Sentence.Verb.Greeting, null));
		AddToBlackboard("investigate", new Sentence(null, Sentence.Verb.investigate, null));
		AddToBlackboard("jail", jail);

		AddToBlackboard("jailSentence", new Sentence(victim.GetComponent<EventManager>().GetKiller(), Sentence.Verb.Arrow, jail));
	}

	public override void constructTree ()
	{
		object[] args = {this.eventManager, this.blackboard, "murderer", "jailPoint", 100}; //TellTravelTo


		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
			//Go To Receptionist
				new Follow(this, receptionistKey),
				new DisplaySentence(this, "hello", 3),
				new Wait(this, 3),
			//Go to corpse, investigate corpse
				//new Follow(this, victimKey),
				new SetPoint(this, "deathPoint", victimKey),
				new FirstToFinish(new Node[] {
					new TravelTo(this, "deathPoint"),
					new Repeater(this, 0, Repeater.RepeatType.UntilSuccess, new Node[] {
						new CheckInRange(this, victimKey, 0.4f),
					})
				}),
				new DisplaySentence(this, "investigate", 3),
				new Wait(this, 3),

				new EnforceResult(true, new Node[] {
					new Sequence(new Node[] {
						new SetLocation(this, "location", "self"),
						new Repeater(this, 0, Repeater.RepeatType.UntilSuccess, new Node[] {
							new SetRandomRoom(this, "destination", "location"),
							new TravelTo(this, "destination"),
							new DisplaySentence(this, "investigate", 3),
							new Wait(this, 3),
						})
					})
				}),
			//Repeat 10 times, go to random room, investigate, interrogate random person
				//Some form of personel investigation?

			//Arrest person, lead them out.
				new Follow(this, "murderer"),
				new SetRandomRoom(this, "jailPoint", "jail"),
				new LinkTree<TellTravelTo>(this, new Node[0], args),
				new DisplaySentence(this, "jailSentence", 3),

				new SetRandomRoom(this, "jailPoint", "jail"),
				new TravelTo(this, "jailPoint"),
				new Kill(this, "murderer"),
                //Kill detective to prevent them from coming over repeatedly. Could be an issue if we have multiple murderes.
                new Kill(this, "self"),
			})
		});
	}
}
