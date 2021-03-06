﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Actions))]
public abstract class EventManager : MonoBehaviour {

	public Sprite liveSprite;
	public Sprite deathSprite;

	private bool alive = true;

	private GameObject killer;
    private GameObject log;

	public struct PrioritisedBehaviour{
		public BehaviourTree behaviourTree;
		public int priority;
	}

	protected List<PrioritisedBehaviour> behaviourQueue = new List<PrioritisedBehaviour>();
	public KnowledgeBase knowledgeBase;
	public Actions actions;

	protected virtual void Awake(){
		GetComponentInChildren<SpriteRenderer>().sprite = liveSprite;
		actions = GetComponent<Actions>();
		knowledgeBase = new KnowledgeBase(this);
        log = GameObject.Find("Log");
    }

	protected virtual void Start(){

	}

	protected virtual void Update(){

	}

	public virtual void TreeCompleted(bool success, BehaviourTree tree){
		//Debug.Log (gameObject + " completed tree " + tree);
		behaviourQueue.RemoveAt(0);
		if(behaviourQueue.Count >= 1 && behaviourQueue[0].behaviourTree != null){
			behaviourQueue[0].behaviourTree.Start();
		}
	}

	public virtual void AcceptKnowledge(Sentence sentence){
		knowledgeBase.addSentence(sentence);
	}

	protected void AddTree(PrioritisedBehaviour prioritisedBehaviour){
		if(alive){

           // Debug.Log (gameObject + " began Tree: " + prioritisedBehaviour.behaviourTree);

            for (int i = 0; i < behaviourQueue.Count; i++){
				if(behaviourQueue[i].priority < prioritisedBehaviour.priority){
					if(i == 0){
						behaviourQueue[0].behaviourTree.root.Interrupt();
					}
					behaviourQueue.Insert(i, prioritisedBehaviour);
					if(i==0 && prioritisedBehaviour.behaviourTree != null){
						behaviourQueue[0].behaviourTree.Start();
					}
					return;
				}
			}

			behaviourQueue.Add(prioritisedBehaviour);
			if(behaviourQueue.Count == 1 && prioritisedBehaviour.behaviourTree != null){
                //Debug.Log(gameObject + " began Tree: " + prioritisedBehaviour.behaviourTree);
                behaviourQueue[0].behaviourTree.Start();
			}

		}
	}

	protected void AddTree(BehaviourTree tree, int priority){
		PrioritisedBehaviour insert = new PrioritisedBehaviour();
		insert.behaviourTree = tree;
		insert.priority = priority;
		AddTree(insert);
	}

	//Another character can push a tree onto this queue - it will only accept if it is the highest priority,
	//unless force is set to true (i.e. set force to true if it's a task to complete, force to false if it's synchronisation stuff)
	public bool PushTree(BehaviourTree tree, int priority, bool force){
		if(behaviourQueue.Count == 0 || behaviourQueue[0].priority < priority || force){
			AddTree(tree, priority);
			return true;
		}
		else{
			//Debug.Log (behaviourQueue[0].priority);
			//Debug.Log(priority);
			return false;
		}
	}

	public void PrunePriorityRange(int max, int min){
		//TODO Can speed up this if needs be
		for(int i = behaviourQueue.Count - 1; i >= 0; i--){
			if(behaviourQueue[i].priority >= min || behaviourQueue[i].priority <= max){
				behaviourQueue.RemoveAt(i);
			}
		}
	}

    public void Kill(GameObject killer) {
        alive = false;
        this.killer = killer;
        //Check that we don't broadcast detective murders since this will break the kill
        //(detective murders have been set in v 2015 as a tool to take inmates out of the game).
        if (killer.GetComponentInChildren<DetectiveManager>() == null)
        { log.GetComponent<log>().Happening(killer.GetComponent<AuthoredGuestManager>().actor_name + " has murder on their mind.", true); }
        if (behaviourQueue.Count > 0){
			behaviourQueue[0].behaviourTree.root.Interrupt();
			behaviourQueue.RemoveRange(0, behaviourQueue.Count);
		}

		GetComponentInChildren<SpriteRenderer>().sprite = deathSprite;

		StartCoroutine("BroadcastDeath", new Sentence(null, Sentence.Verb.Murder, gameObject));
	}
	
	IEnumerator BroadcastDeath(Sentence sentence){
		while(true){
			yield return null;
			actions.BroadcastSentence(sentence, 4);
		}
	}

	public void PruneNullTrees(){
		while(behaviourQueue.Count >= 1 && behaviourQueue[0].behaviourTree == null){
			behaviourQueue.RemoveAt(0);
		}
	}

	public bool Alive(){
		return alive;
	}

	public GameObject GetKiller(){
		return killer;
	}
}
