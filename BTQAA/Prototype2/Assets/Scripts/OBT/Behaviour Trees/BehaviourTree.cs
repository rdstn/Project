using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BehaviourTree{
	public KnowledgeBase knowledgeBase;
	public Actions actions;
	public Dictionary<string, object> blackboard;

	public Root root;

	public EventManager eventManager;

	//private bool constructed = false;

	public BehaviourTree(EventManager eventManager, Dictionary<string, object> blackboard){
		this.eventManager = eventManager;
		this.actions = eventManager.actions;
		this.blackboard = blackboard;
		this.knowledgeBase = eventManager.knowledgeBase;


		AddToBlackboard("self", eventManager.gameObject);
	}

	public BehaviourTree(EventManager eventManager){
		this.eventManager = eventManager;
		this.actions = eventManager.actions;
		this.knowledgeBase = eventManager.knowledgeBase;

		blackboard = new Dictionary<string, object>();

		AddToBlackboard("self", eventManager.gameObject);
	}

	public void AddToBlackboard(string key, object newObject){
		if(blackboard.ContainsKey(key)){
			blackboard[key] = newObject;
		}
		else{
			blackboard.Add(key, newObject);
		}
	}

	public T GetFromBlackboard<T>(string key){
		try{
			T result = (T) blackboard[key];
			return result;
		}
		catch(KeyNotFoundException e){
			Debug.Log (e);
			Debug.Log (key);
			return default(T);
		}
	}

	public void Finished (bool success){
		eventManager.TreeCompleted(success, this);
	}

	public void Start(){
		//if(!constructed){
			constructTree();
		//}
		root.Poke();
	}

	public abstract void constructTree();
}
