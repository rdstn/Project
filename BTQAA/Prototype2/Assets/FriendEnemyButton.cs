using UnityEngine;
using UnityEngine.UI;

public class FriendEnemyButton : MonoBehaviour {

    public UnityEngine.UI.Button self; //self
    public UnityEngine.UI.Text tab; //tab button
    public GameObject carrier; //The data carrier, used for transmission to the actual game session.

	// Use this for initialization
	void Start () {
        self.GetComponentInChildren<Text>().text = tab.text;
        self.image.color = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
        self.GetComponentInChildren<Text>().text = tab.text;
    }

    public void Clicked ()
    {
        if (self.image.color == Color.white)
        {
            self.image.color = Color.red;
        }
        else if (self.image.color == Color.red)
        {
            self.image.color = Color.green;
        }
        else
        {
            self.image.color = Color.white;
        }
    }
}
