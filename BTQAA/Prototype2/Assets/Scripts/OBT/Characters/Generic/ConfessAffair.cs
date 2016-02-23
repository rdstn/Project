using UnityEngine;
using System.Collections;

public class ConfessAffair : BehaviourTree {

	public ConfessAffair(EventManager eventManager, GameObject affair) : base(eventManager){
		AddToBlackboard("affair", affair);
        //Authored
		AddToBlackboard("SO", ((AuthoredGuestManager) eventManager).SO);
        //Unauthored
        //AddToBlackboard("SO", ((GuestManager)eventManager).SO);
        AddToBlackboard("sentence", new Sentence(eventManager.gameObject, Sentence.Verb.Love, affair));
	}

	public override void constructTree ()
	{
		object[] args = {this.eventManager, this.blackboard, "SO", "sentence" ,4, 16}; //speak

		
		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new Follow(this, "SO"),
				new LinkTree<Speak>(this, new Node[0], args),
			}),
		});
	}
}
