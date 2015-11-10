using UnityEngine;
using System.Collections;

public class BroadcastSentence : Leaf_Instant {

	private string sentenceKey;
	private float range;
	
	public BroadcastSentence(BehaviourTree tree, string sentenceKey, float range) : base(tree){
		this.sentenceKey = sentenceKey;
		this.range = range;
	}
	
	public override void Poke (Parent parent)
	{
		base.Poke (parent);
		Sentence sentence = (Sentence) tree.GetFromBlackboard<Sentence>(sentenceKey);
		if(sentence != null){
			tree.actions.BroadcastSentence(sentence, range);
			parent.ReceiveStatus(Status.Success);
		}
		else{
			parent.ReceiveStatus(Status.Failure);
		}
	}
}
