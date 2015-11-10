using UnityEngine;
using System.Collections;

public class WhisperSentence : Leaf_Instant {

	private string sentenceKey;
	private string targetKey;

	private GameObject target;
	
	public WhisperSentence(BehaviourTree tree, string sentenceKey, string targetKey) : base(tree){
		this.sentenceKey = sentenceKey;
		this.targetKey = targetKey;
	}

	public WhisperSentence(BehaviourTree tree, string sentenceKey, GameObject target) : base(tree){
		this.sentenceKey = sentenceKey;
		this.target = target;
	}

	public override void Poke (Parent parent)
	{
		base.Poke (parent);
		Sentence sentence = (Sentence) tree.GetFromBlackboard<Sentence>(sentenceKey);

		if(target == null){
			target = (GameObject) tree.GetFromBlackboard<GameObject>(targetKey);
		}

		if(sentence != null && target != null){
			tree.actions.Whisper(sentence, target);
			parent.ReceiveStatus(Status.Success);
		}
		else{
			parent.ReceiveStatus(Status.Failure);
		}
	}
}
