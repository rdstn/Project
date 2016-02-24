using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoundCorpse : BehaviourTree {

	private string receptionistKey;
	private string sentenceKey;

	public FoundCorpse(EventManager eventManager, GameObject receptionist, Sentence sentence) : base(eventManager){
		receptionistKey = "receptionist";
		sentenceKey = "sentenceKey";
		AddToBlackboard(receptionistKey, receptionist);
		AddToBlackboard(sentenceKey, sentence);
        AddToBlackboard("reception", receptionist);
    }

	public FoundCorpse(EventManager eventManager, Dictionary<string, object> blackboard) : base(eventManager, blackboard){
		
	}

	public override void constructTree ()
	{
		object[] args = {this.eventManager, this.blackboard, receptionistKey, sentenceKey, 5, 20};//Speak
		AddToBlackboard("shock", new Sentence(null, Sentence.Verb.Shocked, null));


        base.root = new Root(this, new Node[] {
            new Sequence(new Node[] {
                new DisplaySentence(this, "shock", 4),
                new Wait(this, 4),
                new SetSpeed(this, 0.4f),
                new Follow(this, receptionistKey),
                new LinkTree<Speak>(this, new Node[0], args),
                //new TravelTo (this, GetFromBlackboard<Vector3>("self")),
			})
		});
	}
}
