using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TabName : MonoBehaviour {

    public Text itself;
    public GameObject carrier;
    public int tabnumber;

	// Use this for initialization
	void Start () {
        carrier = GameObject.Find("Carrier");
        if (tabnumber == 1) {
            itself.text = carrier.GetComponent<CarrierScript>().red1name;
            if (itself.text == "")
            {
                itself.text = "Red1";
            }
        }
        else if (tabnumber == 2)
        {
            itself.text = carrier.GetComponent<CarrierScript>().red2name;
            if (itself.text == "")
            {
                itself.text = "Red2";
            }
        }
        else if (tabnumber == 3)
        {
            itself.text = carrier.GetComponent<CarrierScript>().green1name;
            if (itself.text == "")
            {
                itself.text = "Green1";
            }
        }
        else if (tabnumber == 4)
        {
            itself.text = carrier.GetComponent<CarrierScript>().green2name;
            if (itself.text == "")
            {
                itself.text = "Green2";
            }
        }
        else if (tabnumber == 5)
        {
            itself.text = carrier.GetComponent<CarrierScript>().blue1name;
            if (itself.text == "")
            {
                itself.text = "Blue1";
            }
        }
        if (tabnumber == 6)
        {
            itself.text = carrier.GetComponent<CarrierScript>().blue2name;
            if (itself.text == "")
            {
                itself.text = "Blue2";
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Change ()
    {
        if (tabnumber == 1)
        {
            itself.text = carrier.GetComponent<CarrierScript>().red1name;
        }
        if (tabnumber == 2)
        {
            itself.text = carrier.GetComponent<CarrierScript>().red2name;
        }
        if (tabnumber == 3)
        {
            itself.text = carrier.GetComponent<CarrierScript>().green1name;
        }
        if (tabnumber == 4)
        {
            itself.text = carrier.GetComponent<CarrierScript>().green2name;
        }
        if (tabnumber == 5)
        {
            itself.text = carrier.GetComponent<CarrierScript>().blue1name;
        }
        if (tabnumber == 6)
        {
            itself.text = carrier.GetComponent<CarrierScript>().blue2name;
        }
    }
}
