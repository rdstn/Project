using UnityEngine;
using System.Collections;

public class SetResponse : Leaf_Instant {

	private string inputSentenceKey;
	private string responseKey;

	Sentence inputSentence;

	public SetResponse(BehaviourTree tree, string inputSentenceKey, string responseKey) : base(tree){
		this.inputSentenceKey = inputSentenceKey;
		this.responseKey = responseKey;
	}

	public SetResponse(BehaviourTree tree, Sentence inputSentence, string responseKey) : base(tree){
		this.inputSentence = inputSentence;
		this.responseKey = responseKey;
	}

	public override void Poke (Parent parent)
	{
		base.Poke (parent);

		if(inputSentence == null){
			inputSentence = (Sentence) tree.GetFromBlackboard<Sentence>(inputSentenceKey);
		}

		if(inputSentence != null){
			Sentence response = tree.eventManager.knowledgeBase.GetResponse(inputSentence);
			tree.AddToBlackboard(responseKey, response);
			parent.ReceiveStatus(Status.Success);
		}
		else{
			parent.ReceiveStatus(Status.Failure);
		}
	}
}
