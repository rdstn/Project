using UnityEngine;
using System.Collections;

public class Lover1 : GuestManager {

	bool test = false;

	public GameObject secretLover;

	// Use this for initialization
	protected override void Start () {
		base.Start();

	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	}

	public override void TreeCompleted (bool success, BehaviourTree tree)
	{
		//base.TreeCompleted (success, tree);
		if(!test){
			test = true;
			AddTree(new SecretRomance(this, "lover", secretLover), 11);
		}
		else{
			base.TreeCompleted (success, tree);
		}
	}
}
