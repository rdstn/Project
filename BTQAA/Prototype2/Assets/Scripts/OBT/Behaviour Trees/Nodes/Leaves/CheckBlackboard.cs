using UnityEngine;
using System.Collections;
using System;

public class CheckBlackboard : Leaf_Instant{

	string obj1Key;
	string obj2Key;

	public CheckBlackboard(BehaviourTree tree, string obj1Key, string obj2Key) : 
		base(tree){
		this.obj1Key = obj1Key;
		this.obj2Key = obj2Key;
	}

	public override void Poke (Parent parent)
	{
		base.Poke (parent);
		object obj1 = tree.GetFromBlackboard<object>(obj1Key);
		object obj2 = tree.GetFromBlackboard<object>(obj2Key);
		//if(obj1 == default(T) || obj2 == default(T)){
		//	parent.ReceiveStatus(Status.Failure);
		//}
		//TODO CHECKS
		if(obj1.Equals(obj2)){
			parent.ReceiveStatus(Status.Success);
		}
		else{
			parent.ReceiveStatus(Status.Failure);
		}
	}
}
