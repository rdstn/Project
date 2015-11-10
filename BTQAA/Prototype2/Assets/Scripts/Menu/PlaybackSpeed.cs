using UnityEngine;
using System.Collections;

public class PlaybackSpeed : MonoBehaviour {

	public void PlaybackNormal(){
		Time.timeScale = 1;
	}

	public void PlaybackAdjust(float scale){
		Time.timeScale = scale;
	}
}
