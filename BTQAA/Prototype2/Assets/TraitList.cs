using UnityEngine;
using System.Collections;
using System;
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
        if (character==1)
        {
            //Check if we have the trait already stored.
            if (Array.IndexOf<string>(carrier.GetComponent<CarrierScript>().red1traits,trait) != -1)
            {
                set = true;
                opposite.interactable = false;
                opposite.image.color = Color.red;
                itself.image.color = Color.green;
            }
        }
        if (character==2)
        {
            if (Array.IndexOf<string>(carrier.GetComponent<CarrierScript>().red2traits, trait) != -1)
            {
                set = true;
                opposite.interactable = false;
                opposite.image.color = Color.red;
                itself.image.color = Color.green;
            }
        }
        if (character==3)
        {
            if (Array.IndexOf<string>(carrier.GetComponent<CarrierScript>().green1traits, trait) != -1)
            {
                set = true;
                opposite.interactable = false;
                opposite.image.color = Color.red;
                itself.image.color = Color.green;
            }
        }
        if (character==4)
        {
            if (Array.IndexOf<string>(carrier.GetComponent<CarrierScript>().green2traits, trait) != -1)
            {
                set = true;
                opposite.interactable = false;
                opposite.image.color = Color.red;
                itself.image.color = Color.green;
            }
        }
        if (character==5)
        {
            if (Array.IndexOf<string>(carrier.GetComponent<CarrierScript>().blue1traits, trait) != -1)
            {
                set = true;
                opposite.interactable = false;
                opposite.image.color = Color.red;
                itself.image.color = Color.green;
            }
        }
        if (character==6)
        {
            if (Array.IndexOf<string>(carrier.GetComponent<CarrierScript>().blue2traits, trait) != -1)
            {
                set = true;
                opposite.interactable = false;
                opposite.image.color = Color.red;
                itself.image.color = Color.green;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Check that we can only set up to 4 traits.
        if (carrier.GetComponent<CarrierScript>().traitcounters[character-1]==4)
        {
            if (set == false)
            {
                itself.image.color = Color.red;
                itself.interactable = false;
            }
        }
        //If we have less than 4 traits (or somehow got 4+) and we don't have the opposite set, enable the button.
        else
        {
            if (set == false && opposite.GetComponent<TraitList>().set == false)
            {
                itself.image.color = Color.white;
                itself.interactable = true;
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

