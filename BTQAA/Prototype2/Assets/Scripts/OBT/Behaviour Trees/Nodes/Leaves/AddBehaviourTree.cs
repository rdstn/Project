using UnityEngine;
using System.Collections;
using System;

public class AddBehaviourTree<T> : Leaf_Instant where T : BehaviourTree{

	string targetKey;
	object [] args;
	int priority;
	bool force;

	//Bit frustrating how this works, sorry about that. Args must be all the args you will need apart from the eventManager,
	//which will be put at the front of this list. :(

	public AddBehaviourTree(BehaviourTree tree, string targetKey, int priority, bool force, object[] args) : base(tree){
		this.targetKey = targetKey;
		this.args = args;
		this.priority = priority;
		this.force = force;
	}

	public override void Poke (Parent parent)
	{
		base.Poke (parent);
		GameObject target = tree.GetFromBlackboard<GameObject>(targetKey);
		if(target != null){
			EventManager targetManager = target.GetComponent<EventManager>();
			if(targetManager != null){
				object[] fullArgs = new object[1 + args.Length];
				fullArgs[0] = targetManager;
				args.CopyTo(fullArgs, 1);

				T newTree = (T) Activator.CreateInstance(typeof(T), fullArgs);

				if(targetManager.PushTree(newTree, priority, force)){
					//Debug.Log ("Success");
					parent.ReceiveStatus(Status.Success);
					return;
				}
				//Debug.Log ("Failed Because push didn't work");
			}
			//Debug.Log ("Failed Because target didn't work secondTime time");
		}
		//Debug.Log ("Failed Because target didn't work first time");
		//Debug.Log ("Fail");
		parent.ReceiveStatus(Status.Failure);
	}
}
