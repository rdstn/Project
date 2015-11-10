using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class Actions : MonoBehaviour {

	public GameObject hoverSentencePrefab;

	private NavMeshAgent nav;

	private Collider[] locations;

	void Awake(){
		nav = GetComponent<NavMeshAgent>();

		GameObject locationsContainer = GameObject.FindGameObjectWithTag ("locationsList");
		locations = locationsContainer.GetComponentsInChildren <Collider>();
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//--------------------------------------------------------

	public void Repeat(Repeater repeater){
		StartCoroutine("RepeatRoutine", repeater);
	}
	
	IEnumerator RepeatRoutine(Repeater repeater){
		yield return null;
		yield return null;
		
		repeater.Repeat();
	}
	
	public void RepeatInterrupt(){
		StopCoroutine("RepeatRoutine");
	}

	//--------------------------------------------------------

	public void Wait(float waitTime, Wait waitNode){
		StartCoroutine("WaitRoutine", new object[] {waitTime, waitNode});
	}
	
	IEnumerator WaitRoutine(object[] args){
		float waitTime = (float) args[0];
		Wait waitNode = (Wait) args[1];

		float waitedTime = 0;
		while(true){
			waitedTime += Time.deltaTime;
			if(waitedTime < waitTime){
				yield return null;
			}
			else{
				waitNode.ReceiveStatus(Node.Status.Success);
				break;
			}
		}
	}

	public void WaitInterrupt(){
		StopCoroutine("WaitRoutine");
	}

	public void WaitUntilTime(float waitTime, WaitUntilTime waitNode){
		StartCoroutine("WaitUntilTimeRoutine", new object[] {waitTime, waitNode});
	}

	IEnumerator WaitUntilTimeRoutine(object[] args){
		float waitTime = (float) args[0];
		WaitUntilTime waitNode = (WaitUntilTime) args[1];

		while(true){
			if(Time.timeSinceLevelLoad < waitTime){
				yield return null;
			}
			else{
				waitNode.ReceiveStatus(Node.Status.Success);
				break;
			}
		}
	}

	public void WaitUntilTimeInterrupt(){
		StopCoroutine("WaitUntilTimeRoutine");
	}

	//--------------------------------------------------------

	public bool IsInRange(GameObject target, float range){
		if(Vector3.Distance(this.gameObject.transform.position, target.transform.position) < range){
			return true;
		}
		else{
			return false;
		}
	}

	//--------------------------------------------------------

	public void SetSpeed(float speed){
		nav.speed = speed;
	}

	//--------------------------------------------------------

	public void TravelTo(Vector3 destination, TravelTo travelToNode){
		nav.SetDestination(destination);
		nav.Resume();
		StartCoroutine("TravelToRoutine", travelToNode);
	}

	IEnumerator TravelToRoutine(TravelTo travelToNode){
		while(true){
			//Debug.Log (gameObject);
			//Debug.Log (nav.destination);
			//Debug.Log (transform.position);
			if(nav.remainingDistance > nav.stoppingDistance || nav.pathPending){
				yield return null;
			}
			else{
				travelToNode.ReceiveStatus(Node.Status.Success);
				break;
			}
		}
	}

	public void TravelToInterrupt(){
		StopCoroutine("TravelToRoutine");
		nav.Stop();
	}

	//--------------------------------------------------------

	public void Follow(GameObject target, Follow followNode, float stopping){
		nav.SetDestination(target.transform.position);
		nav.Resume();
		StartCoroutine("FollowRoutine", new object[] {target, followNode, stopping});
	}
	
	IEnumerator FollowRoutine(object[] args){
		GameObject target = (GameObject) args[0];
		Follow followNode = (Follow) args[1];
		float stopping = (float) args[2];

		while(true){
			if(!target.GetComponent<EventManager>().Alive()){
				nav.Stop();
				followNode.ReceiveStatus(Node.Status.Failure);
				break;
			}
			if(nav.remainingDistance > (nav.stoppingDistance + stopping) || nav.pathPending){
				if(target == null){
					followNode.ReceiveStatus(Node.Status.Failure);
					break;
				}
				nav.SetDestination(target.transform.position);
				yield return null;
			}
			else{
				nav.Stop ();
				followNode.ReceiveStatus(Node.Status.Success);
				break;
			}
		}
	}

	public void FollowInterrupt(){
		StopCoroutine ("FollowRoutine");
		nav.Stop();
	}

	//--------------------------------------------------------

	public GameObject FindRoomTargetIsIn(GameObject target){
		foreach(Collider location in locations){
			if(location.gameObject.GetComponent<Contents>().Contains(target)){
				return location.gameObject;
			}
		}
		
		return null;
	}

	public GameObject GetAloneWith(GameObject location){
		if(location.GetComponent<Contents>().GetContentsWithout(this.gameObject).Count == 1){
			return location.GetComponent<Contents>().GetContentsWithout(this.gameObject)[0];
		}
		
		return null;
	}

	public GameObject GetEmptyRoom(){
		List<Collider> potentialLocations = new List<Collider>();

		foreach(Collider location in locations){
			if(location.gameObject.GetComponent<Contents>().GetContents().Count == 0 && location.name != "jail"){
				potentialLocations.Add(location);
			}
		}

		if (potentialLocations.Count == 0) {
			return null;
		}
		else{
			return potentialLocations[Random.Range(0, potentialLocations.Count)].gameObject;
		}
	}

	//--------------------------------------------------------

	public void Kill(GameObject victim, Kill killNode){
		EventManager victimEventManager = victim.GetComponent<EventManager>();
		if(victimEventManager == null || !victimEventManager.Alive()){
			killNode.ReceiveStatus(Node.Status.Failure);
		}
		else{
			victimEventManager.Kill(this.gameObject);
			killNode.ReceiveStatus(Node.Status.Success);
		}
	}

	//--------------------------------------------------------

	public Vector3 RandomPointInRandomRoom(){
		int newIndex = Random.Range (0, locations.Length - 1);
		
		Collider newLocation = locations [newIndex];
		
		float minX = newLocation.bounds.min.x;
		float maxX = newLocation.bounds.max.x;
		float minZ = newLocation.bounds.min.z;
		float maxZ = newLocation.bounds.max.z;
		
		return new Vector3(Random.Range (minX, maxX), 0, Random.Range(minZ, maxZ));
	}

	public Vector3 RandomPointInRoom(GameObject room){
		Collider area = room.GetComponent<Collider>();

		float minX = area.bounds.min.x;
		float maxX = area.bounds.max.x;
		float minZ = area.bounds.min.z;
		float maxZ = area.bounds.max.z;
		
		return new Vector3(Random.Range (minX, maxX), 0, Random.Range(minZ, maxZ));
	}

	//--------------------------------------------------------

	public void DisplaySentence(Sentence sentence, float seconds){
		GameObject hoverSentence = Instantiate(hoverSentencePrefab) as GameObject;
		hoverSentence.GetComponent<HoverSentence>().ConstructSentence(sentence, this.gameObject, seconds);
	}

	public void BroadcastSentence(Sentence sentence, float range){
		gameObject.GetComponent<EventManager>().AcceptKnowledge(sentence);
		Collider[] listeners = Physics.OverlapSphere (gameObject.transform.position, range);
		foreach(Collider collider in listeners){
			if(collider.gameObject.tag == "character" && collider.gameObject != this.gameObject){
				RaycastHit hit;
				Vector3 origin = this.gameObject.transform.position + new Vector3(0f, 0.5f, 0f);
				Vector3 direction = collider.gameObject.transform.position - this.gameObject.transform.position;
				
				if(Physics.Raycast(origin, direction.normalized, out hit, range, ~(1 << 2))){
					if(hit.collider.gameObject.GetComponent<EventManager>() != null){
						if(hit.collider.gameObject.GetComponent<EventManager>().Alive ()){
							hit.collider.gameObject.GetComponent<EventManager>().AcceptKnowledge(sentence);
						}
					}
				}
			}
		}
	}

	public bool Whisper(Sentence sentence, GameObject target){
		if (target.GetComponent<EventManager> ().Alive ()) {
			target.GetComponent<EventManager> ().AcceptKnowledge(sentence);
			return true;
		} 
		else {
			return false;
		}
	}

	public void SaySentence(Sentence sentence, GameObject target, float seconds){
		DisplaySentence(sentence, seconds);
		//Whisper and somehow return information?
	}

	public void ShoutSentence(Sentence sentence, float range, float seconds){
		DisplaySentence(sentence, seconds);
		BroadcastSentence(sentence, range);
	}

	//--------------------------------------------------------
}
