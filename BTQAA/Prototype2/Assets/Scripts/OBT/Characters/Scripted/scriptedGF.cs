using UnityEngine;
using System.Collections;

public class scriptedGF : BehaviourTree {
	
	//GameObject[] locations;
	GameObject[] characters;
	
	public scriptedGF(EventManager manager, GameObject[] locations, GameObject[] characters) : base(manager){
		//this.locations = locations;
		this.characters = characters; 

		AddToBlackboard("myRoom", locations[18]);
		AddToBlackboard("redMale", characters[2]);
		AddToBlackboard("redFemale", characters[3]);
		AddToBlackboard("yellowMale", characters[6]);
	}
	
	public override void constructTree ()
	{
		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new WaitUntilTime(this, 24.4f),

				new SetRandomRoom(this, "destination", "myRoom"),
				new TravelTo(this, "destination"),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Bag, null), 3f),
				new WaitUntilTime(this, 65.5f),

				new TravelTo(this, new Vector3(1.09f, 0, 5.16f)),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Love, null), 11f),
				new WaitUntilTime(this, 85.5f),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Love, null), 11f),
				new Wait(this, 15),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Love, null), 11f),
				new Wait(this, 15),

				new WaitUntilTime(this, 154),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Burger, null), 5f),
				new WaitUntilTime(this, 160),
				new TravelTo(this, new Vector3(-0.1f, 0f, 3.07f)),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Shocked, null), 3f),
				new Wait(this, 6.5f),
				new SetSpeed(this, 0.4f),
				new Follow(this, "redMale"),
				new SetSpeed(this, 0.25f),
				new DisplaySentence(this, new Sentence(characters[3], Sentence.Verb.Love, characters[6]), 4f),
				new Wait(this, 4),
				new TravelTo(this, new Vector3(-5.28f, 0f, -1.27f)),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Love, null), 2f),
			})
		});
	}
}
