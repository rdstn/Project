using UnityEngine;
using System.Collections;

public class scriptedRF : BehaviourTree {
	
	//GameObject[] locations;
	//GameObject[] characters;
	
	public scriptedRF(EventManager manager, GameObject[] locations, GameObject[] characters) : base(manager){
		//this.locations = locations;
		//this.characters = characters; 

		AddToBlackboard("myRoom", locations[17]);
	}
	
	public override void constructTree ()
	{
		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new WaitUntilTime(this, 23.9f),

				new SetRandomRoom(this, "destination", "myRoom"),
				new TravelTo(this, "destination"),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Bag, null), 3f),
				new WaitUntilTime(this, 54.5f),

				new Wait(this, 3f),
				new TravelTo(this, new Vector3(2.98f, 0, -0.575f)),
				new Wait(this, 1f),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Beer, null), 3f),
				new Wait(this, 3f),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Beer, null), 3f),

				new WaitUntilTime(this, 123f),
				new Wait(this, 3f),
				new TravelTo(this, new Vector3(1.33f, 0, -1.41f)),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Toilet, null), 3f),
				new Wait(this, 3f),
				new TravelTo(this, new Vector3(0.30f, 0f, -0.422f)),

				new WaitUntilTime(this, 146f),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Love, null), 10f),
				new Wait(this, 13f),
				new SetRandomRoom(this, "destination", "myRoom"),
				new TravelTo(this, "destination"),

				new Wait(this, 4f),
				new SetRandomRoom(this, "destination", "myRoom"),
				new TravelTo(this, "destination"),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Shocked, null), 5f),
				new Wait(this, 5f),
				new SetRandomRoom(this, "destination", "myRoom"),
				new TravelTo(this, "destination"),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Shocked, null), 5f),
				new Wait(this, 5f),
				new SetRandomRoom(this, "destination", "myRoom"),
				new TravelTo(this, "destination"),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Shocked, null), 5f),
				new Wait(this, 5f),
				new SetRandomRoom(this, "destination", "myRoom"),
				new TravelTo(this, "destination"),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Shocked, null), 5f),
				new Wait(this, 5f),
				new SetRandomRoom(this, "destination", "myRoom"),
				new TravelTo(this, "destination"),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Shocked, null), 5f),
				new Wait(this, 5f),
				new SetRandomRoom(this, "destination", "myRoom"),
				new TravelTo(this, "destination"),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Shocked, null), 5f),
				new Wait(this, 5f),
				new SetRandomRoom(this, "destination", "myRoom"),
				new TravelTo(this, "destination"),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Shocked, null), 5f),
				new Wait(this, 5f),
				new SetRandomRoom(this, "destination", "myRoom"),
				new TravelTo(this, "destination"),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Shocked, null), 5f),
				new Wait(this, 5f),
			})
		});
	}
}
