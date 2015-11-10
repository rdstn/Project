using UnityEngine;
using System.Collections;

public class scriptedRM : BehaviourTree {
	
	GameObject[] locations;
	GameObject[] characters;
	
	public scriptedRM(EventManager manager, GameObject[] locations, GameObject[] characters) : base(manager){
		this.locations = locations;
		this.characters = characters; 

		AddToBlackboard("myRoom", locations[17]);
		AddToBlackboard("yellowMale", characters[6]);
	}
	
	public override void constructTree ()
	{
		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new TravelTo(this, new Vector3(-0.2f, 0f, -2.1f)),
				new DisplaySentence(this, new Sentence(characters[2], Sentence.Verb.StayingIn, null), 4f),
				new WaitUntilTime(this, 15.5f),
				new TravelTo(this, new Vector3(1.01f, 0f, -2.34f)),
				new DisplaySentence(this, new Sentence(characters[2], Sentence.Verb.StayingIn, locations[17]) ,4f),
				new DisplaySentence(this, new Sentence(characters[4], Sentence.Verb.StayingIn, locations[18]) ,4f),
				new DisplaySentence(this, new Sentence(characters[6], Sentence.Verb.StayingIn, locations[19]) ,4f),
				new WaitUntilTime(this, 23.9f),

				new SetRandomRoom(this, "destination", "myRoom"),
				new TravelTo(this, "destination"),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Bag, null), 3f),
				new WaitUntilTime(this, 54.5f),

				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Beer, null),3f),
				new TravelTo(this, new Vector3(2.67f, 0, -0.58f)),
				new Wait(this, 2f),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Beer, null), 3f),
				new Wait(this, 3f),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Beer, null), 3f),

				new WaitUntilTime(this, 123f),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Burger, null), 3f),
				new Wait(this, 3f),
				new TravelTo(this, new Vector3(1.33f, 0, -1.72f)),
				new Wait(this, 3f),

				new TravelTo(this, new Vector3(-5.77f, 0f, -2.28f)),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Burger, null), 3f),
				new Wait(this, 3f),
				new TravelTo(this, new Vector3(-5.28f, 0f, -1.27f)),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Burger, null), 3f),

				new WaitUntilTime(this, 210),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Shocked, null), 5f),
				new Wait(this, 5f),

				new SetSpeed(this, 0.5f),
				new Follow(this, "yellowMale"),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Shocked, null), 4f),
				new Wait(this, 4f),
				new Kill(this, "yellowMale"),
				new Wait(this, 4f),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Shocked, null), 4f),
				new SetSpeed(this, 0.25f),

				new TravelTo(this, new Vector3(-3.31f, 0f, 0.72f)),
				new WaitUntilTime(this, 417),
				new SetSpeed(this, 0.45f),
				new TravelTo(this, new Vector3(2.54f, 0, -7.08f)),

			})
		});
	}
}
