using UnityEngine;
using System.Collections;

public class SnookerShot : BehaviourTree {

	float minWait = 0.5f;
	float maxWait = 1.5f;

	GameObject table;

	int priority;

	public SnookerShot(EventManager eventManager, GameObject table, GameObject opponent, int priority) : base(eventManager){
		AddToBlackboard("table", table);
		AddToBlackboard("opponent", opponent);
		AddToBlackboard("pool", new Sentence(null, Sentence.Verb.Pool, null));

		this.table = table;
		this.priority = priority;
	}

	public override void constructTree ()
	{
		GameObject self = GetFromBlackboard<GameObject>("self");
		object[] args = {table, self, priority} ;//Addsnookershot

		base.root = new Root(this, new Node[] {
			new Sequence(new Node[] {
				new SetRandomRoom(this, "destination", "table"),
				new TravelTo(this, "destination"),
				new Wait(this, minWait, maxWait),
				new DisplaySentence(this, "pool", 1),
				new RandomFail(this, 0.1f),
				new AddBehaviourTree<SnookerShot>(this, "opponent", priority, false, args),
			})
		});
	}
}
