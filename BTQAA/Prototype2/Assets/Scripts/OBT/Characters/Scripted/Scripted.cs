using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Scripted : EventManager {

	public int scriptSelection;

	public GameObject location;
	private GameObject[] locations;
	
	public GameObject character;
	private GameObject[] characters;

	protected override void Awake ()
	{
		List<Transform> locationTransforms = new List<Transform>();
		foreach(Transform t in location.transform){
			locationTransforms.Add(t);
		}
		locations = new GameObject[locationTransforms.Count];
		for(int i = 0; i < locationTransforms.Count; i++){
			locations[i] = locationTransforms[i].gameObject;
		}


		List<Transform> characterTransforms = new List<Transform>();
		foreach(Transform t in character.transform){
			characterTransforms.Add(t);
		}
		characters = new GameObject[characterTransforms.Count];
		for(int i = 0; i < characterTransforms.Count; i++){
			characters[i] = characterTransforms[i].gameObject;
		}

		base.Awake ();
	}
	
	// Use this for initialization
	protected override void Start () {

		switch(scriptSelection)
		{

		case 1:
			AddTree(new scriptedReceptionist(this, locations, characters), 1);
			break;

		case 2:
			AddTree(new scriptedDetective(this, locations, characters), 1);
			break;

		case 3:
			AddTree(new scriptedRM(this, locations, characters), 1);
			break;

		case 4:
			AddTree(new scriptedRF(this, locations, characters), 1);
			break;

		case 5:
			AddTree(new scriptedGM(this, locations, characters), 1);
			break;

		case 6:
			AddTree(new scriptedGF(this, locations, characters), 1);
			break;

		case 7:
			AddTree(new scriptedYM(this, locations, characters), 1);
			break;

		case 8:
			AddTree(new scriptedYF(this, locations, characters), 1);
			break;


		}

		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	}
}
