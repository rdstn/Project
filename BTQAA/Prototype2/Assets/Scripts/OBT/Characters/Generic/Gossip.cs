using UnityEngine;
using System.Collections;

public class Gossip : BehaviourTree {

	public Gossip(EventManager eventManager, GameObject target, Sentence gossip) : base(eventManager){
		AddToBlackboard("target", target);
		AddToBlackboard("gossip", gossip);
	}

	public override void constructTree ()
	{
		object[] args = {this.eventManager, this.blackboard, "target", "gossip", 4,12}; //Speak

		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new Follow(this, "target"),
				new LinkTree<Speak>(this, new Node[0], args)
			})
		});
	}
}
