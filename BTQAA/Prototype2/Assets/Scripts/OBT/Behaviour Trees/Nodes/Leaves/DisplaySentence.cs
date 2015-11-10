using UnityEngine;
using System.Collections;

public class DisplaySentence : Leaf_Instant {

	private string sentenceKey;
	private Sentence sentence;
	private float seconds;
	
	public DisplaySentence(BehaviourTree tree, string sentenceKey, float seconds) : base(tree){
		this.sentenceKey = sentenceKey;
		this.seconds = seconds;
	}

	public DisplaySentence(BehaviourTree tree, Sentence sentence, float seconds) : base(tree){
		this.sentence = sentence;
		this.seconds = seconds;
	}
	
	public override void Poke (Parent parent)
	{
		base.Poke (parent);
		if(sentence == null){
			sentence = (Sentence) tree.GetFromBlackboard<Sentence>(sentenceKey);
		}
		if(sentence != null){
			tree.actions.DisplaySentence(sentence, seconds);
			parent.ReceiveStatus(Status.Success);
		}
		else{
			parent.ReceiveStatus(Status.Failure);
		}
	}
}
