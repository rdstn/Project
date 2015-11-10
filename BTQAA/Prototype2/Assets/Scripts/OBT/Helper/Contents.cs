using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Contents : MonoBehaviour {
	
	private List<GameObject> contents;
	
	void Awake(){
		contents = new List<GameObject>();
	}
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other){
		contents.Add(other.gameObject);
	}
	
	void OnTriggerExit(Collider other){
		contents.Remove (other.gameObject);
	}
	
	public List<GameObject> GetContents(){
		PurgeList ();
		return contents;
	}
	
	public bool Contains(GameObject gameObject){
		PurgeList ();
		if(contents.Contains(gameObject)){
			return true;
		}
		else{
			return false;
		}
	}
	
	public List<GameObject> GetContentsWithout(GameObject gameObject){
		PurgeList ();
		if(!Contains (gameObject)){
			return contents;
		}
		else{
			List<GameObject> clone = new List<GameObject>();
			foreach(GameObject obj in contents){
				clone.Add(obj);
			}
			clone.Remove(gameObject);
			return clone;
		}
	}

	//Removes dead characters
	public void PurgeList(){
		for(int i = contents.Count - 1; i >=0; i--){
			EventManager instanceEventManager = GetComponent<EventManager>();
			if(instanceEventManager != null){
				if(instanceEventManager.Alive() == false){
					contents.RemoveAt(i);
				}
			}
		}
	}
}
