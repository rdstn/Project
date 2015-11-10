using UnityEngine;
using System.Collections;

public class ConstructSentence : Leaf_Instant {

	string noun1Key;
	Sentence.Verb verb;
	string noun2Key;

	string sentenceKey;

	public ConstructSentence(BehaviourTree tree, string noun1Key, Sentence.Verb verb, string noun2Key, string sentenceKey) : base(tree){
		this.noun1Key = noun1Key;
		this.verb = verb;
		this.noun2Key = noun2Key;

		this.sentenceKey = sentenceKey;
	}

	public override void Poke (Parent parent)
	{
		base.Poke (parent);

		GameObject noun1 = tree.GetFromBlackboard<GameObject>(noun1Key);
		GameObject noun2 = tree.GetFromBlackboard<GameObject>(noun2Key);

		if(noun1 != null && noun2 != null){
			tree.AddToBlackboard(sentenceKey, new Sentence(noun1, verb, noun2));
			parent.ReceiveStatus(Status.Success);
		}
		else{
			parent.ReceiveStatus(Status.Failure);
		}
	}
}
