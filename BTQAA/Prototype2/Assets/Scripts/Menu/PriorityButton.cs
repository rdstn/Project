using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PriorityButton : MonoBehaviour {

    public Button itself; //Refers to the button which we are attaching the script to.
    public int character; //Refers to the character whose priorites we are editing.
    public int priority; //Refers to the current priority setting.
    public int tree; //Refers to the tree which we are editing. A commented list of trees is available in CarrierScript.cs
    public int character_status; //Refers if we are setting priorities for enemies, friends or neutrals.
    public GameObject carrier; //Refers to the carrier object.

	// Use this for initialization
	void Start () {
        carrier = GameObject.Find("Carrier");
        itself.GetComponentInChildren<Text>().text = priority.ToString();
        if (character_status == 0)
        {
            itself.image.color = Color.white;
        }
        else if (character_status == 1)
        {
            itself.image.color = Color.red;
        }
        else
        {
            itself.image.color = Color.green;
        }
        //Check what's already stored on the carrier.
        if (character == 1)
        {
            itself.GetComponentInChildren<Text>().text = carrier.GetComponent<CarrierScript>().red1priorities[tree,character_status].ToString();
        }
        else if (character == 2)
        {
            itself.GetComponentInChildren<Text>().text = carrier.GetComponent<CarrierScript>().red2priorities[tree,character_status].ToString();
        }
        else if (character == 3)
        {
            itself.GetComponentInChildren<Text>().text = carrier.GetComponent<CarrierScript>().green1priorities[tree,character_status].ToString();
        }
        else if (character == 4)
        {
            itself.GetComponentInChildren<Text>().text = carrier.GetComponent<CarrierScript>().green2priorities[tree,character_status].ToString();
        }
        else if (character == 5)
        {
            itself.GetComponentInChildren<Text>().text = carrier.GetComponent<CarrierScript>().blue1priorities[tree,character_status].ToString();
        }
        else if (character == 6)
        {
            itself.GetComponentInChildren<Text>().text = carrier.GetComponent<CarrierScript>().blue2priorities[tree,character_status].ToString();
        }
    }

    public void Clicked ()
    {
        if (priority<5)
        {
            priority++;
            itself.GetComponentInChildren<Text>().text = priority.ToString();
            carrier.GetComponent<CarrierScript>().ChangePriority(priority, tree, character, character_status);
        }
        else if (priority == 5)
        {
            priority = 0;
            itself.GetComponentInChildren<Text>().text = "-";
            carrier.GetComponent<CarrierScript>().ChangePriority(priority, tree, character, character_status);
        }
        else
        {
            Debug.Log("You have an unexpected priority: " + priority);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
