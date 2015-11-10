using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public Text text;
	
	// Update is called once per frame
	void Update () {
		text.text = Time.timeSinceLevelLoad.ToString();
		if(Time.timeSinceLevelLoad > 439){
			Application.LoadLevel(0);
		}
	}
}
