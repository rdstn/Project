using UnityEngine;
using System.Collections;

public abstract class Node {
	public enum Status {Success, Failure};

	protected BehaviourTree tree; //The Tree this Node is a part of. Used to access knowledge and actions.
	protected Parent parent;

	public virtual void Poke(Parent parent){
		this.parent = parent;
	}

	public abstract void Interrupt();
}
