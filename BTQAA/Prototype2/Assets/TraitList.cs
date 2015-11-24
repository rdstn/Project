using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TraitList : MonoBehaviour
{
    public Button itself; //References the object this script is attached to.
    public Button opposite; //References the opposite trait.
    public bool set; //References the state of the button - set or not.
    public GameObject carrier; //References the carrier object.
    public int character; //References the character whose traits we are editing.
    public string trait; //The trait manipulated by this object.

    // Use this for initialization
    void Start()
    {
        itself.GetComponentInChildren<Text>().text = trait;
        itself.image.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (carrier.GetComponent<CarrierScript>().traitcounters[character-1]==3)
        {
            if (set== false)
            {
                itself.image.color = Color.red;
                itself.interactable = false;
            }
        }
    }

    public void Press ()
    {
        if (set == false)
        {
            set = true;
            carrier.GetComponent<CarrierScript>().SetTrait(trait, character);
            opposite.interactable = false;
            opposite.image.color = Color.red;
            itself.image.color = Color.green;
        }
        else if (set == true)
        {
            set = false;
            carrier.GetComponent<CarrierScript>().RemoveTrait(trait, character);
            opposite.interactable = true;
            opposite.image.color = Color.white;
            itself.image.color = Color.white;
        }
    }

}

