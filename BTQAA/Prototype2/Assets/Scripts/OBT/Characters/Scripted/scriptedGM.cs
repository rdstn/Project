using UnityEngine;
using System.Collections;

public class scriptedGM : BehaviourTree {
	
	GameObject[] locations;
	GameObject[] characters;
	
	public scriptedGM(EventManager manager, GameObject[] locations, GameObject[] characters) : base(manager){
		this.locations = locations;
		this.characters = characters; 

		AddToBlackboard("myRoom", locations[18]);
		AddToBlackboard("YellowRoom", locations[19]);
		AddToBlackboard("redMale", characters[2]);
		AddToBlackboard("receptionist", characters[0]);
	}
	
	public override void constructTree ()
	{
		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new WaitUntilTime(this, 10.5f),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Greeting, null), 3f),
				new WaitUntilTime(this, 23.9f),

				new SetRandomRoom(this, "destination", "myRoom"),
				new TravelTo(this, "destination"),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Bag, null), 3f),
				new WaitUntilTime(this, 65.5f),

				new TravelTo(this, new Vector3(0.77f, 0, 5.16f)),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Love, null), 11f),
				new WaitUntilTime(this, 85.5f),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Love, null), 11f),
				new Wait(this, 15),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Love, null), 11f),
				new Wait(this, 15),

				new WaitUntilTime(this, 160),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Sleep, null), 20f),

				new WaitUntilTime(this, 248),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Shocked, null), 1f),
				new Wait(this, 1f),
				new TravelTo(this, new Vector3(1.83f, 0, 4.4f)),
				new Wait(this, 2f),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Shocked, null), 3f),
				new Wait(this, 3f),
				new TravelTo(this, new Vector3(0.82f, 0, 4.02f)),
				new Wait(this, 16f),
				new Follow(this, "receptionist"),
				new DisplaySentence(this, new Sentence(characters[2], Sentence.Verb.Murder, null), 5f),
				new DisplaySentence(this, new Sentence(characters[2], Sentence.Verb.StayingIn, locations[19]), 5f),
				new Wait(this, 8),
				new TravelTo(this, new Vector3(-4.55f, 0, -0.52f)),
			})
		});
	}
}
