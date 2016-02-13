using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class log : MonoBehaviour {

    public GameObject itself;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Happening(string logNew, bool critical)
    {
        var log = itself.GetComponentsInChildren<Text>();
        log[0].text = log[1].text;
        log[1].text = log[2].text;
        log[2].text = logNew;
        if (critical== true)
        {
            log[2].color = Color.red;
        }
        if (critical == false)
        {
            log[2].color = Color.black;
        }
    }
}
