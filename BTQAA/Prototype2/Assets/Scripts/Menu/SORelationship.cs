using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SORelationship : MonoBehaviour
{
    public Dropdown itself; //References the object we tied this script to.
    public int character; //References which character's relationship we are editing.
    public int target; //References the S.O. with whom the relationship is.
    public GameObject carrier;

    void Start()
    {
        carrier = GameObject.Find("Carrier");
    }

    public void Change(int input)
    {
        carrier.GetComponent<CarrierScript>().SetRelationship(input, target, character);
    }
}
