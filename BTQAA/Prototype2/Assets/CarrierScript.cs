using UnityEngine;
using System.Collections;

[System.Serializable]
public class CarrierScript : MonoBehaviour {

    public static CarrierScript current;
    public int[] red1traits;
    public int[] red2traits;
    public int[] green1traits;
    public int[] green2traits;
    public int[] blue1traits;
    public int[] blue2traits;
    public int[] red1priorities;
    public int[] red2priorities;
    public int[] green1priorities;
    public int[] green2priorities;
    public int[] blue1priorities;
    public int[] blue2priorities;
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
        int size = 10; //depends on the number of behaviour trees for which we set up priorities.
        red1traits = new int[4];
        red2traits = new int[4];
        green1traits = new int[4];
        green2traits = new int[4];
        blue1traits = new int[4];
        blue2traits = new int[4];
        red1priorities = new int[size];
        red2priorities = new int[size];
        green1priorities = new int[size];
        green2priorities = new int[size];
        blue1priorities = new int[size];
        blue2priorities = new int[size];
        red1relationships = new int[5];
        red2relationships = new int[5];
        green1relationships = new int[5];
        green2relationships = new int[5];
        blue1relationships = new int[5];
        blue2relationships = new int[5];

    }
	
	//Update is called once per frame
	void Update () {
	
	}

    //We need to keep this object on loading the scene.
    void Awake()
    {
        DontDestroyOnLoad (transform.gameObject);
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
    public void SetTrait (int trait, int character, int number)
    {
        if (character == 1)
        {
            red1traits[number] = trait;
        }
        else if (character == 2)
        {
            red2traits[number] = trait;
        }
        else if (character == 3)
        {
            green1traits[number] = trait;
        }
        else if (character == 4)
        {
            green2traits[number] = trait;
        }
        else if (character == 5)
        {
            blue1traits[number] = trait;
        }
        else if (character == 6)
        {
            blue2traits[number] = trait;
        }
        else
        {
            Debug.Log("Adding traits to a non-existent actor! " + character);
        }

    }

    //Adding relationships to actors, in RGB order, R1 to B2.
    public void SetRelationship (int status, int target, int character)
    {
        if (character == 1)
        {
            red1relationships[target] = status;
        }
        else if (character == 2)
        {
            red2relationships[target] = status;
        }
        else if (character == 3)
        {
            green1relationships[target] = status;
        }
        else if (character == 4)
        {
            green2relationships[target] = status;
        }
        else if (character == 5)
        {
            blue1relationships[target] = status;
        }
        else if (character == 6)
        {
            blue2relationships[target] = status;
        }
        else
        {
            Debug.Log("Adding a relationship to a non-existent actor! " + character);
        }
    }
// TODO: add the same for priorities when the list of updateable priorities has been decided upon.
}
