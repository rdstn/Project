using UnityEngine;
using System.Collections;

public class scriptedReceptionist : BehaviourTree {

	GameObject[] locations;
	GameObject[] characters;

	public scriptedReceptionist(EventManager manager, GameObject[] locations, GameObject[] characters) : base(manager){
		this.locations = locations;
		this.characters = characters;
	}

	public override void constructTree ()
	{
		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new WaitUntilTime(this, 12.5f),
				new DisplaySentence(this, new Sentence(characters[2], Sentence.Verb.StayingIn, locations[17]) ,4f),
				new DisplaySentence(this, new Sentence(characters[4], Sentence.Verb.StayingIn, locations[18]) ,4f),
				new DisplaySentence(this, new Sentence(characters[6], Sentence.Verb.StayingIn, locations[19]) ,4f),

				new WaitUntilTime(this, 311),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Shocked, null), 4f),
				new Wait(this, 10f),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.police, null), 4f),
				new Wait(this, 4f),
				new DisplaySentence(this, new Sentence(characters[2], Sentence.Verb.Murder, null), 4f),
				new Wait(this, 4f),

				new WaitUntilTime(this, 342),
				new DisplaySentence(this, new Sentence(null, Sentence.Verb.Greeting, null), 3f),
			})
		});
	}
}
