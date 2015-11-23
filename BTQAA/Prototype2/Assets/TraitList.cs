using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TraitList : MonoBehaviour {
    public Dropdown itself; //References the object this script is attached to.
    public Dropdown[] other_traits; //References the other trait selection dropdowns.
    public GameObject carrier; //References the carrier object.
    public int character; //References the character whose traits we are editing.
    //TODO: Rework to use the carrier, because otherwise it's problematic.
	// Use this for initialization
	void Start () {
        //Traits work in pairs (except for none) - traits in a pair are mutually exclusive.
        string[] new_options = { "None",
            "Brave", "Craven",
            "Outgoing", "Shy",
            "Amorous", "Chaste",
            "Gluttonous", "Temperate",
            "Greedy", "Charitable",
            "Lazy", "Energetic",
            "Wrathful", "Patient",
            "Envious", "Kind",
            "Proud", "Humble",
            "Paranoid", "Trusting",
            "Cruel", "Honorable" };
        for (int i=0; i<=new_options.Length; i++)
        {
            itself.options[i].text = new_options[i];
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Select (int input)
    {
        //This is needed for mutual exclusiviy of traits.
        for (int i = 0; i < other_traits.Length; i++)
        {
            other_traits[i].GetComponent<TraitList>().Change(itself.options[input].text);
        }
    }


    //Call when we make a change. The input will be the index of the option selected.
    public void Change (string input)
    {
        ArrayList options = new ArrayList { "None",
            "Brave", "Craven",
            "Outgoing", "Shy",
            "Amorous", "Chaste",
            "Gluttonous", "Temperate",
            "Greedy", "Charitable",
            "Lazy", "Energetic",
            "Wrathful", "Patient",
            "Envious", "Kind",
            "Proud", "Humble",
            "Paranoid", "Trusting",
            "Cruel", "Honorable" };
        if (options.Contains(input) && input!="None")
        {
            int selection=options.IndexOf(input);
            options.Remove(input);
            if (selection % 2 != 0)
            {
                options.RemoveAt(selection + 1);
            }
            else
            {
                options.RemoveAt(selection - 1);
            }
        }
        string [] new_options = (string[]) options.ToArray(typeof(string));
        for (int i = 0; i <= itself.options.Count/2; i++)
        {
            itself.options[i].text = new_options[i];
        }

    }
    }

