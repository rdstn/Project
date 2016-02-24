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
    private string currentToolTipText = "";
    private GUIStyle guiStyleFore;
    private GUIStyle guiStyleBack;
    private string tooltip;

    // Use this for initialization
    void Start()
    {
        itself.GetComponentInChildren<Text>().text = trait;
        itself.image.color = Color.white;
        carrier = GameObject.Find("Carrier");
        guiStyleFore = new GUIStyle();
        guiStyleFore.normal.textColor = Color.white;
        guiStyleFore.normal.background = MakeTex(1, 1, Color.black);
        guiStyleFore.alignment = TextAnchor.UpperCenter;
        guiStyleFore.wordWrap = true;
        guiStyleBack = new GUIStyle();
        guiStyleBack.normal.textColor = Color.black;
        guiStyleBack.alignment = TextAnchor.UpperCenter;
        guiStyleBack.wordWrap = true;
        if (trait=="Brave")
        {
            tooltip = "This character is extra confrontational and willing to take risks.";
        }
        if (trait == "Craven")
        {
            tooltip = "This character tries to avoid any confrontation.";
        }
        if (trait == "Outgoing")
        {
            tooltip = "This character feels at home when spending time with others.";
        }
        if (trait == "Shy")
        {
            tooltip = "This character will try their best to avoid social interactions.";
        }
        if (trait == "Amorous")
        {
            tooltip = "This character is a lover, not a figther.";
        }
        if (trait == "Chaste")
        {
            tooltip = "This character is faithful.";
        }
        if (trait == "Gluttonous")
        {
            tooltip = "This character really likes their food.";
        }
        if (trait == "Temperate")
        {
            tooltip = "This character knows measure when it comes to food and drink.";
        }
        if (trait == "Greedy")
        {
            tooltip = "This character is ready to sell their kids, for the right price.";
        }
        if (trait == "Charitable")
        {
            tooltip = "For this character, sharing is more important than posessions.";
        }
        if (trait == "Lazy")
        {
            tooltip = "This character would rather just not be bothered with it.";
        }
        if (trait == "Energetic")
        {
            tooltip = "This character is active and aims to do as much as possible.";
        }
        if (trait == "Wrathful")
        {
            tooltip = "This character's ire is easily attracted and quite deadly.";
        }
        if (trait == "Patient")
        {
            tooltip = "This character has a good tolerance for things not going their way.";
        }
        if (trait == "Envious")
        {
            tooltip = "This character will have the most, or else.";
        }
        if (trait == "Kind")
        {
            tooltip = "This character is a kind soul.";
        }
        if (trait == "Proud")
        {
            tooltip = "This character has a very high opinion of themselves.";
        }
        if (trait == "Humble")
        {
            tooltip = "This character dislikes being in the spotlights.";
        }
        if (trait == "Paranoid")
        {
            tooltip = "This character knows that everyone's after them.";
        }
        if (trait == "Trusting")
        {
            tooltip = "This character believes that everyone else is well-meaning";
        }
        if (trait == "Cruel")
        {
            tooltip = "This character is quite aggressive towards their enemies.";
        }
        if (trait == "Honorable")
        {
            tooltip = "This character does not speak ill, even of his enemies.";
        }
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

    public void MouseIn()
    {
        currentToolTipText = tooltip;
        //print(currentToolTipText);
    }

    public void MouseOut()
    {
        currentToolTipText = "";
    }

    void OnGUI()
    {
        if (currentToolTipText != "")
        {
            var x = Event.current.mousePosition.x;
            var y = Event.current.mousePosition.y;
            GUI.Label(new Rect(x - 149, y + 40, 300, 30), currentToolTipText, guiStyleBack);
            GUI.Label(new Rect(x - 150, y + 40, 300, 30), currentToolTipText, guiStyleFore);
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

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];

        for (int i = 0; i < pix.Length; i++)
            pix[i] = col;

        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();

        return result;
    }

}

