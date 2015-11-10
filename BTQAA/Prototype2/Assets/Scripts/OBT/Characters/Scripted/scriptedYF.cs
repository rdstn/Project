using UnityEngine;
using System.Collections;

public class scriptedYF : BehaviourTree {
	
	GameObject[] locations;
	GameObject[] characters;
	
	public scriptedYF(EventManager manager, GameObject[] locations, GameObject[] characters) : base(manager){
		this.locations = locations;
		this.characters = characters; 

		AddToBlackboard("myRoom", locations[19]);
	}
	
	public override void constructTree ()
	{
		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new WaitUntilTime(this, 10),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Greeting, null), 3f),
				new WaitUntilTime(this, 25.0f),

				new SetRandomRoom(this, "destination", "myRoom"),
				new TravelTo(this, "destination"),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Bag, null), 3f),
				new WaitUntilTime(this, 76.5f),

				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Burger, null), 3),
				new Wait(this, 3),
				new TravelTo(this, new Vector3(3.0f, 0f, 3.9f)),
				new Wait(this, 6),

				new TravelTo(this, new Vector3(-5.77f, 0f, -2.28f)),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Burger, null), 3f),
				new Wait(this, 3f),
				new TravelTo(this, new Vector3(-4.716f, 0f, -1.245f)),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Burger, null), 3f),

				new WaitUntilTime(this, 210),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Shocked, null), 5f),
				new Wait(this, 5f),

				new WaitUntilTime(this, 408f),
				new DisplaySentence(this, new Sentence(characters[2], Sentence.Verb.StayingIn, locations[7]), 3f),
			})
		});
	}
}
