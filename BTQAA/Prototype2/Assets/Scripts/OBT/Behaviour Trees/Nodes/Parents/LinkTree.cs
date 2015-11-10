using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

//TODO Find out if there's a smaller include.

public class LinkTree<T> : Parent where T : BehaviourTree{

	object[] args;

	public LinkTree(BehaviourTree tree, Node[] children, object[] args) : base(children){
		base.tree = tree;
		this.args = args;
	}

	public override void Poke (Parent parent)
	{
		base.Poke (parent);
		if(children.Count == 0){
			T subTree = (T) Activator.CreateInstance(typeof(T), args);
			subTree.constructTree();
			SetChild(subTree.root);
		}
		children[0].Poke(this);
	}

	public override void ReceiveStatus (Status status)
	{
		parent.ReceiveStatus(status);
	}
}
