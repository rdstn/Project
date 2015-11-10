using UnityEngine;
using System.Collections;

public class scriptedYM : BehaviourTree {
	
	//GameObject[] locations;
	//GameObject[] characters;
	
	public scriptedYM(EventManager manager, GameObject[] locations, GameObject[] characters) : base(manager){
		//this.locations = locations;
		//this.characters = characters; 

		AddToBlackboard("myRoom", locations[19]);
	}
	
	public override void constructTree ()
	{
		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new TravelTo(this, new Vector3(1.04f, 0f, -2.51f)),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Greeting, null), 3f),
				new WaitUntilTime(this, 26.4f),

				new SetRandomRoom(this, "destination", "myRoom"),
				new TravelTo(this, "destination"),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Bag, null), 3f),
				new WaitUntilTime(this, 76.5f),
				new Wait(this, 3),
				new TravelTo(this, new Vector3(3.97f, 0f, 4.03f)),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Toilet, null), 3f),
				new Wait(this, 15),
				new TravelTo(this, new Vector3(0.37f, 0f, -0.7f)),

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

				new TravelTo(this, new Vector3(3.97f, 0f, 4.03f)),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Toilet, null), 5f),
				new WaitUntilTime(this, 246),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Shocked, null), 5f),
			})
		});
	}
}
