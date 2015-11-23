using UnityEngine;
using UnityEngine.UI;

public class FriendEnemyButton : MonoBehaviour {

    public Button itself; //self
    public Text tab; //tab button text referential to this character button.
    public int target; //marks which relationship we are editing.
    public int character; //marks which character we are editing.
    public GameObject carrier; //The data carrier, used for transmission to the actual game session.

    // Use this for initialization
    void Start () {
        itself.GetComponentInChildren<Text>().text = tab.text;
        itself.image.color = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
        itself.GetComponentInChildren<Text>().text = tab.text;
    }

    public void Clicked ()
    {
        if (itself.image.color == Color.white)
        {
            itself.image.color = Color.red;
            carrier.GetComponent<CarrierScript>().SetRelationship(2, target, character);
        }
        else if (itself.image.color == Color.red)
        {
            itself.image.color = Color.green;
            carrier.GetComponent<CarrierScript>().SetRelationship(1, target, character);
        }
        else
        {
            itself.image.color = Color.white;
            carrier.GetComponent<CarrierScript>().SetRelationship(0, target, character);
        }
    }
}
