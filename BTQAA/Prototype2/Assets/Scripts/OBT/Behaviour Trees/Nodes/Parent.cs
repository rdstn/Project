using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Parent : Node{
	protected List<Node> children = new List<Node>();

	public Parent(Node[] children){
		if(children.Length == 1){
			SetChild(children[0]);
		}
		else{
			AddChildren(children);
		}
	}

	public void AddChildren(Node[] children){
		for(int i=0; i < children.Length; i++){
			this.children.Add(children[i]);
		}
	}

	public void SetChild(Node child){
		children.Capacity = 1;
		if(children.Count == 0){
			children.Add(child);
		}
		else{
			children[0] = child;
		}
	}

	public Node GetChild(int index){
		return children[index];
	}

	public abstract void ReceiveStatus(Node.Status status);

	public override void Interrupt(){
		for(int i=0; i < children.Count; i++){
			children[i].Interrupt();
		}
	}
}
