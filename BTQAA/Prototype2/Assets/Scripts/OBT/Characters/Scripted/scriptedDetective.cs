using UnityEngine;
using System.Collections;

public class scriptedDetective : BehaviourTree {
	
	GameObject[] locations;
	GameObject[] characters;
	
	public scriptedDetective(EventManager manager, GameObject[] locations, GameObject[] characters) : base(manager){
		this.locations = locations;
		this.characters = characters; 

		AddToBlackboard("redMale", characters[2]);
	}
	
	public override void constructTree ()
	{
		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new WaitUntilTime(this, 330),
				new TravelTo(this, new Vector3(-0.05f, 0, -2.23f)),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Greeting, null), 3f),
				new Wait(this, 6f),

				new TravelTo(this, new Vector3(3.6f, 0f, 4.03f)),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.investigate, null), 6f),
				new Wait(this, 6f),

				new TravelTo(this, new Vector3(-4.43f, 0f, -0.91f)),
				new DisplaySentence(this, new Sentence(characters[2], Sentence.Verb.StayingIn, null), 3f),
				new Wait(this, 6f),

				new Follow(this, "redMale"),
				new DisplaySentence(this, new Sentence(characters[2], Sentence.Verb.Arrow, locations[23]), 5f),
				new Wait(this, 2f),
				new TravelTo(this, new Vector3(2.54f, 0, -7.28f)),

			})
		});
	}
}
