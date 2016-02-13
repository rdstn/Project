using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NameField : MonoBehaviour {

    public InputField itself;
    public GameObject carrier;
    public int tabnumber;
    public Text tabname;


	// Use this for initialization
	void Start () {
        carrier = GameObject.Find("Carrier");
        //See if we loaded in a value for this from the carrier.
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

    public void Change ()
    {
        if (tabnumber == 1)
        {
            carrier.GetComponent<CarrierScript>().Rename(itself.text, 1);
            tabname.text = itself.text;
        }
        if (tabnumber == 2)
        {
            carrier.GetComponent<CarrierScript>().Rename(itself.text, 2);
            tabname.text = itself.text;
        }
        if (tabnumber == 3)
        {
            carrier.GetComponent<CarrierScript>().Rename(itself.text, 3);
            tabname.text = itself.text;
        }
        if (tabnumber == 4)
        {
            carrier.GetComponent<CarrierScript>().Rename(itself.text, 4);
            tabname.text = itself.text;
        }
        if (tabnumber == 5)
        {
            carrier.GetComponent<CarrierScript>().Rename(itself.text, 5);
            tabname.text = itself.text;
        }
        if (tabnumber == 6)
        {
            carrier.GetComponent<CarrierScript>().Rename(itself.text, 6);
            tabname.text = itself.text;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
