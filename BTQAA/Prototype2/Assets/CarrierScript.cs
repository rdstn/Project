using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
public class CarrierScript : MonoBehaviour {

    public GameObject itself; //Refernces the carrier this script is attached to.
    public static CarrierScript current;
    public int[] traitcounters;
    public string[] red1traits;
    public string[] red2traits;
    public string[] green1traits;
    public string[] green2traits;
    public string[] blue1traits;
    public string[] blue2traits;
    public int[,] red1priorities;
    public int[,] red2priorities;
    public int[,] green1priorities;
    public int[,] green2priorities;
    public int[,] blue1priorities;
    public int[,] blue2priorities;
    public int[] red1relationships;
    public int[] red2relationships;
    public int[] green1relationships;
    public int[] green2relationships;
    public int[] blue1relationships;
    public int[] blue2relationships;
    public string red1name;
    public string red2name;
    public string green1name;
    public string green2name;
    public string blue1name;
    public string blue2name;


    // Use this for initialization
    void Start () {
        

    }
	
	//Update is called once per frame
	void Update () {
	
	}

    //The carrier needs to awaken first, so those variable declarations are best stored in awake, NOT start.
    //We need to keep this object on loading the scene.
    void Awake()
    {
        DontDestroyOnLoad (itself);
        int size = 7; //depends on the number of behaviour trees for which we set up priorities.
        int[] traitcounters = new int[] { 0, 0, 0, 0, 0, 0 }; //Keep in store how many traits does a character have. We want a limit of 4.
        red1traits = new string[4];
        red2traits = new string[4];
        green1traits = new string[4];
        green2traits = new string[4];
        blue1traits = new string[4];
        blue2traits = new string[4];
        red1priorities = new int[size, 3];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                red1priorities[i, j] = 3;
            }
        }
        red2priorities = new int[size, 3];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                red2priorities[i, j] = 3;
            }
        }
        green1priorities = new int[size, 3];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                green1priorities[i, j] = 3;
            }
        }
        green2priorities = new int[size, 3];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                green2priorities[i, j] = 3;
            }
        }
        blue1priorities = new int[size, 3];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                blue1priorities[i, j] = 3;
            }
        }
        blue2priorities = new int[size, 3];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                blue2priorities[i, j] = 3;
            }
        }
        red1relationships = new int[6];
        red2relationships = new int[6];
        green1relationships = new int[6];
        green2relationships = new int[6];
        blue1relationships = new int[6];
        blue2relationships = new int[6];
    }

    //Renaming the actors, in RGB order, Red 1 to Blue 2.
    public void Rename(string newname, int character)
    {
        if (character == 1)
        {
            red1name = newname;
        }
        else if (character == 2)
        {
            red2name = newname;
        }
        else if (character == 3)
        {
            green1name = newname;
        }
        else if (character == 4)
        {
            green2name = newname;
        }
        else if (character == 5)
        {
            blue1name = newname;
        }
        else if (character == 6)
        {
            blue2name = newname;
        }
        else
        {
            Debug.Log("Renaming a non-existent actor! " + character);
        }
    }

    //Adding traits to actors, in RGB order, R1 to B2.
    public void SetTrait (string trait, int character)
    {
        if (character == 1)
        {
            red1traits[0] = trait; //Since we sort empty strings to front, we can just push in the new trait to front.
            Array.Sort(red1traits);            
        }
        else if (character == 2)
        {
            red2traits[0] = trait;
            Array.Sort(red2traits);
        }
        else if (character == 3)
        {
            green1traits[0] = trait;
            Array.Sort(green1traits);
        }
        else if (character == 4)
        {
            green2traits[0] = trait;
            Array.Sort(green2traits);
        }
        else if (character == 5)
        {
            blue1traits[0] = trait;
            Array.Sort(blue1traits);
        }
        else if (character == 6)
        {
            blue2traits[0] = trait;
            Array.Sort(blue2traits);
        }
        else
        {
            Debug.Log("Adding traits to a non-existent actor! " + character);
        }
        traitcounters[character - 1]++; //Counter for how many traits a character has (up to 4, as per TraitList.cs).
    }

    public void RemoveTrait (string trait, int character)
    {
        if (character == 1)
        {
            int index = Array.IndexOf<string>(red1traits, trait); //Find the trait.
            red1traits[index] = ""; //Remove it.
            Array.Sort(red1traits); //Push empties to front.
            
        }
        else if (character == 2)
        {
            int index = Array.IndexOf<string>(red2traits, trait);
            red2traits[index] = "";
            Array.Sort(red2traits);
        }
        else if (character == 3)
        {
            int index = Array.IndexOf<string>(green1traits, trait);
            green1traits[index] = "";
            Array.Sort(green1traits);
        }
        else if (character == 4)
        {
            int index = Array.IndexOf<string>(green2traits, trait);
            green2traits[index] = "";
            Array.Sort(green2traits);
        }
        else if (character == 5)
        {
            int index = Array.IndexOf<string>(blue1traits, trait);
            blue1traits[index] = "";
            Array.Sort(blue1traits);
        }
        else if (character == 6)
        {
            int index = Array.IndexOf<string>(blue2traits, trait);
            blue2traits[index] = "";
            Array.Sort(blue2traits);
        }
        else
        {
            Debug.Log("Removing traits from a non-existent actor! " + character);
        }
        traitcounters[character - 1]--;
    }

    //Adding relationships to actors, in RGB order, R1 to B2.
    public void SetRelationship (int status, int target, int character)
    {
        //Since we do targets 1-6 and the array starts at 0, we need to decrement the target int.
        if (character == 1)
        {
            red1relationships[target-1] = status;
        }
        else if (character == 2)
        {
            red2relationships[target-1] = status;
        }
        else if (character == 3)
        {
            green1relationships[target-1] = status;
        }
        else if (character == 4)
        {
            green2relationships[target-1] = status;
        }
        else if (character == 5)
        {
            blue1relationships[target-1] = status;
        }
        else if (character == 6)
        {
            blue2relationships[target-1] = status;
        }
        else
        {
            Debug.Log("Adding a relationship to a non-existent actor! " + character);
        }
    }

    //Get the prirority value, the tree which we set a priority for, which character we are doing it for
    //and wether or not this applies to friends, enemies or neutral characters.
    public void ChangePriority (int priority, int tree, int character, int target_status)
    {
        if (character == 1)
        {
            red1priorities[tree,target_status] = priority;
        }
        else if (character == 2)
        {
            red2priorities[tree, target_status] = priority;
        }
        else if (character == 3)
        {
            green1priorities[tree, target_status] = priority;
        }
        else if (character == 4)
        {
            green2priorities[tree, target_status] = priority;
        }
        else if (character == 5)
        {
            blue1priorities[tree, target_status] = priority;
        }
        else if (character == 6)
        {
            blue2priorities[tree, target_status] = priority;
        }
        else
        {
            Debug.Log("Changing the priorities of a non-existent actor! " + character);
        }
    }
}
